﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Enums.EnumExtensions;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;
using Services.Interfaces.HospitalRegistrationsService;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.MailboxModels;

namespace Services.HospitalRegistrationsService
{
    public class HospitalRegistrationsService : IHospitalRegistrationsService
    {
        private readonly IEmptyPlaceStatisticRepository _emptyPlaceStatisticRepository;
        private readonly IEmptyPlaceByTypeStatisticRepository _emptyPlaceByTypeStatisticRepository;
        private readonly IHospitalSectionProfileRepository _hospitalSectionProfileRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHospitalUserRepository _hospitalUserRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IHospitalManager _hospitalManager;

        public HospitalRegistrationsService(IEmptyPlaceStatisticRepository emptyPlaceStatisticRepository,
            IHospitalSectionProfileRepository hospitalSectionProfileRepository, ITokenManager tokenManager,
            IHospitalUserRepository hospitalUserRepository, IEmptyPlaceByTypeStatisticRepository emptyPlaceByTypeStatisticRepository, IReservationRepository reservationRepository, IUserRepository userRepository, IMessageRepository messageRepository, IPatientRepository patientRepository, IHospitalManager hospitalManager)
        {
            _emptyPlaceStatisticRepository = emptyPlaceStatisticRepository;
            _hospitalSectionProfileRepository = hospitalSectionProfileRepository;
            _tokenManager = tokenManager;
            _hospitalUserRepository = hospitalUserRepository;
            _emptyPlaceByTypeStatisticRepository = emptyPlaceByTypeStatisticRepository;
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _patientRepository = patientRepository;
            this._hospitalManager = hospitalManager;
        }

        public GetChangeHospitalRegistrationsPageInformationCommandAnswer GetChangeHospitalRegistrationsPageInformation(
            GetChangeHospitalRegistrationsPageInformationCommand command)
        {
            const int days = 30;
            var now = DateTime.Now;
            var deadLine = now + new TimeSpan(days, 0, 0, 0);

            var startMonday = GetPreviousMonday(now);
            var endMonday = GetPreviousMonday(deadLine);
            var weeks = (endMonday - startMonday).Days/7 + 1;

            var user = _tokenManager.GetUserByToken(command.Token);
            var hospitalId = GetHospitalIdByUserId(user.Id);
            var completeCount = this.GetHospitalProfileCount(hospitalId);
            var statisticList = this.GetStatisticList(now, deadLine, hospitalId);

            var startSchedule = Enumerable.Range(0, weeks)
                .Select(week => new ScheduleTableItem
                {
                    Cells = Enumerable.Range(0, 7)
                        .ToDictionary(day => (DayOfWeek) day, day => new ScheduleTableCell
                        {
                            IsBlocked =
                                startMonday.AddDays(7*week + day).Date < now.Date ||
                                startMonday.AddDays(7*week + day).Date > deadLine.Date,
                            Day = startMonday.AddDays(7*week + day).Day,
                            IsCompleted =
                                statisticList.Count(model => model.Date.Date == startMonday.AddDays(7*week + day).Date) ==
                                completeCount,
                            IsStarted =
                                statisticList.Any(model => model.Date.Date == startMonday.AddDays(7*week + day).Date),
                            IsThisMonth = startMonday.AddDays(7*week + day).Month == now.Month,
                            IsThisDate = startMonday.AddDays(7*week + day).Date == now.Date,
                            Date = startMonday.AddDays(7*week + day).Date
                        })
                })
                .ToList();

            return new GetChangeHospitalRegistrationsPageInformationCommandAnswer
            {
                Token = (Guid) command.Token,
                Schedule = startSchedule
            };
        }

        protected virtual List<EmptyPlaceStatisticStorageModel> GetStatisticList(DateTime startDate,
            DateTime endDate, int hospitalId)
        {
            var emptyPlaceStatistics = _emptyPlaceStatisticRepository.GetModels();
            var hospitalSectionProfiles = _hospitalSectionProfileRepository.GetModels();

            var results = from emptyPlaceStatistic in emptyPlaceStatistics
                where emptyPlaceStatistic.Date >= startDate && emptyPlaceStatistic.Date <= endDate
                join hospitalSectionProfile in hospitalSectionProfiles on emptyPlaceStatistic.HospitalSectionProfileId
                    equals hospitalSectionProfile.Id
                where hospitalSectionProfile.HospitalId == hospitalId
                select emptyPlaceStatistic;

            return results.ToList();
        }

        protected virtual int GetHospitalProfileCount(int hospitalId)
        {
            return _hospitalSectionProfileRepository.GetModels().Count(model => model.HospitalId == hospitalId);
        }

        protected virtual int GetHospitalIdByUserId(int userId)
        {
            var result = _hospitalUserRepository.GetModels().FirstOrDefault(model => model.Id == userId).HospitalId;

            return result;
        }

        protected virtual DateTime GetPreviousMonday(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            const DayOfWeek monday = DayOfWeek.Monday;

            var dayDifference = (int)dayOfWeek - (int)monday;
            dayDifference = dayDifference < 0 ? 7 + dayDifference : dayDifference;

            var previousMonday = date - new TimeSpan(dayDifference, 0, 0, 0);

            return previousMonday;
        }

        public ShowHospitalRegistrationPlacesByDateCommandAnswer ShowHospitalRegistrationPlacesByDate(
            ShowHospitalRegistrationPlacesByDateCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);
            var hospitalId = GetHospitalIdByUserId(user.Id);

            var hospitalSectionProfiles = _hospitalSectionProfileRepository.GetModels();
            
            var date = command.Date.Date;
            var statisticList = this.GetStatisticList(date, date, hospitalId);
            var completeCount = this.GetHospitalProfileCount(hospitalId);

            var table = ((IDbSet<HospitalSectionProfileStorageModel>) hospitalSectionProfiles)
                .Where(model => model.HospitalId == hospitalId)
                .Where(model => model.EmptyPlaceStatistics.Any(storageModel => storageModel.Date == command.Date))
                .Select(model => new HospitalRegistrationTableItem
                {
                    HospitalProfileId = model.Id,
                    HospitalProfileName = model.Name,
                    StatisticItems = model.EmptyPlaceStatistics
                        .Where(storageModel => storageModel.Date == command.Date)
                        .SelectMany(storageModel => storageModel.EmptyPlaceByTypeStatistics)
                        .Select(storageModel => new HospitalRegistrationCountStatisticItem
                        {
                            Sex = storageModel.Sex,
                            OpenCount = storageModel.Count
                         })
                        .ToList()
                }).ToList();

            var statistics = statisticList.Count(model => model.Date.Date == date);

            return new ShowHospitalRegistrationPlacesByDateCommandAnswer
            {
                Token = (Guid) command.Token,
                Date = command.Date,
                Table = table,
                IsCompleted = statistics == completeCount,
                HasDialogMessage = command.HasDialogMessage != null && command.HasDialogMessage.Value,
                DialogMessage = command.DialogMessage
            };
        }

        public ChangeHospitalRegistrationForSelectedSectionCommandAnswer ChangeHospitalRegistrationForSelectedSection(
            ChangeHospitalRegistrationForSelectedSectionCommand command)
        {
            var hospitalSectionProfilesName = _hospitalSectionProfileRepository.GetModels()
                .FirstOrDefault(model=>model.SectionProfileId == command.HospitalProfileId).Name;

            var date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var emptyPlaceStatisticRepository = _emptyPlaceByTypeStatisticRepository.GetModels();
            
            var emptyPlaceId = _emptyPlaceStatisticRepository.GetModels()
                .FirstOrDefault(model => model.HospitalSectionProfileId == command.HospitalProfileId && model.Date == date)
                .Id;
            
            var table = emptyPlaceStatisticRepository
                .Where(model => model.EmptyPlaceStatistic.HospitalSectionProfileId == command.HospitalProfileId)
                .Where(storageModel => storageModel.EmptyPlaceStatisticId.Equals(emptyPlaceId))
                .Select(model => new HospitalRegistrationCountStatisticItem
                {
                    Sex = model.Sex,
                    OpenCount = model.Count,
                    Id = model.Id,
                    RegisteredCount = model.Reservations.Count(storageModel => storageModel.Status == ReservationStatus.Opened)
                }).ToList();

            return new ChangeHospitalRegistrationForSelectedSectionCommandAnswer
            {
                Token = (Guid)command.Token,
                StatisticItems = table,
                Date = command.Date,
                SectionProfileName = hospitalSectionProfilesName,
                HospitalProfileId = command.HospitalProfileId
            };
        }

        private List<CommandAnswerError> ValidateApplyChangesHospitalRegistrationCommand(
            GetChangeHospitalRegistrationCommand command)
        {
            List<CommandAnswerError> list = new List<CommandAnswerError>();
            foreach (HospitalRegistrationCountStatisticItem element in command.FreeHospitalSectionsForRegistration)
            {
                if (element.OpenCount < 0)
                    list.Add(new CommandAnswerError
                    {
                        FieldName = "Число свободных мест", 
                        Title = "Число свободных мест не может быть отрицательным"
                    });

            }
            return list;
        }

        public GetChangeHospitalRegistrationCommandAnswer ApplyChangesHospitalRegistration(
            GetChangeHospitalRegistrationCommand command)
        {
            DateTime date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            
            var errors = this.ValidateApplyChangesHospitalRegistrationCommand(command);
           
            if (errors.Any())
            {
                return new GetChangeHospitalRegistrationCommandAnswer
                {
                    Date = command.Date,
                    Token = command.Token.Value,
                    Errors = errors
                };
            }

            var emptyPlaceStatisticsId =
            _emptyPlaceStatisticRepository.GetModels()
                .FirstOrDefault(model => model.HospitalSectionProfileId == command.HospitalProfileId && model.Date == date)
                .Id;

            var freeHospitalSectionsForRegistrationList = command.FreeHospitalSectionsForRegistration;

            var freeHospitalSectionsForRegistrationInTable =
                ((IDbSet<EmptyPlaceByTypeStatisticStorageModel>) _emptyPlaceByTypeStatisticRepository.GetModels())
                    .Where(model => model.EmptyPlaceStatisticId == emptyPlaceStatisticsId).ToList();

            var result = freeHospitalSectionsForRegistrationList
                .Select(element => freeHospitalSectionsForRegistrationInTable
                    .Where(model => element != null 
                        && model.Sex.ToString() == element.Sex.ToString())
                        .Select(model => new EmptyPlaceByTypeStatisticStorageModel
                        {
                            Id = model.Id, 
                            Count = element.OpenCount, 
                            Sex = element.Sex, 
                            EmptyPlaceStatisticId = emptyPlaceStatisticsId
                        }).FirstOrDefault()).ToList();

            foreach (var emptyPlace in result)
            {
                _emptyPlaceByTypeStatisticRepository.Update(emptyPlace.Id, emptyPlace);
            }
            _emptyPlaceByTypeStatisticRepository.SaveChanges();
     
            var messageText = $"Количество свободных мест для {command.SectionProfileName} успешно изменено";

            return new GetChangeHospitalRegistrationCommandAnswer
            {
                Token = (Guid)command.Token,
                HasDialogMessage = true,
                DialogMessage = messageText
            };
        }

        public ChangeHospitalRegistrationForNewSectionCommandAnswer ChangeHospitalRegistrationForNewSection(
            ChangeHospitalRegistrationForNewSectionCommand command)
        {
            return new ChangeHospitalRegistrationForNewSectionCommandAnswer
            {
                Token = (Guid)command.Token,
                FreeHospitalSectionsForRegistration = GetFreeSectionsList(command.Date, command.Token.Value),
                Date = command.Date
            };
        }

        private List<HospitalSectionProfileStorageModel> GetFreeSectionsList(string Date, Guid token)
        {
            var date = DateTime.ParseExact(Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var user = _tokenManager.GetUserByToken(token);
            var hospitalId = GetHospitalIdByUserId(user.Id);

            var hospitalSectionProfiles = _hospitalSectionProfileRepository.GetModels();

            var hospitalSectionProfilesList = _hospitalSectionProfileRepository.GetModels().Where(model => model.HospitalId == hospitalId).ToList();

            var table = ((IDbSet<HospitalSectionProfileStorageModel>)hospitalSectionProfiles)
                .Where(model => model.HospitalId == hospitalId)
                .Where(model => model.EmptyPlaceStatistics.Any(storageModel => storageModel.Date == date))
                .Select(model => new HospitalRegistrationTableItem
                {
                    HospitalProfileId = model.Id,
                    HospitalProfileName = model.Name
                }).ToList();
            

            return hospitalSectionProfilesList.Where(model => table.All(t => t.HospitalProfileId != model.Id)).ToList();
        }

        private List<CommandAnswerError> ValidateApplyChangesNewHospitalRegistrationCommand(
            GetChangeNewHospitalRegistrationCommand command)
        {
            List<CommandAnswerError> list = new List<CommandAnswerError>();
            foreach (HospitalRegistrationCountStatisticItem element in command.CountFreePlaces)
            {
                if (element.OpenCount < 0)
                    list.Add(new CommandAnswerError
                    {
                        FieldName = "Число свободных мест (Пол: " + element.Sex?.ToCorrectString() + ")",
                        Title = "Число свободных мест не может быть отрицательным"
                    });

            }
            return list;
        }

        public GetChangeNewHospitalRegistrationCommandAnswer ApplyChangesNewHospitalRegistration(
            GetChangeNewHospitalRegistrationCommand command)
        {
            var date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            
            var errors = this.ValidateApplyChangesNewHospitalRegistrationCommand(command);
           
            if (errors.Any())
            {
                return new GetChangeNewHospitalRegistrationCommandAnswer
                {
                    Date = command.Date,
                    Token = command.Token.Value,
                    Errors = errors,
                    FreeHospitalSectionsForRegistration = GetFreeSectionsList(command.Date, command.Token.Value)
                };
            }
            var hospitalSectionProfileName =
                _hospitalSectionProfileRepository.GetModels().FirstOrDefault(model => model.Id == command.HospitalProfileId).Name;

            var recordExists =
                _emptyPlaceStatisticRepository.GetModels()
                    .Any(model => model.Date == date && model.HospitalSectionProfileId == command.HospitalProfileId);

            if (!recordExists)
            {
                var newHospitalSectionProfileId = new EmptyPlaceStatisticStorageModel
                {
                    Date = date,
                    CreateTime = DateTime.Now,
                    HospitalSectionProfileId = command.HospitalProfileId,
                    EmptyPlaceByTypeStatistics = command.CountFreePlaces
                        .Select(pair => new EmptyPlaceByTypeStatisticStorageModel
                        {
                            Sex = pair.Sex,
                            Count = pair.OpenCount
                        }
                        ).ToList()
                };

                _emptyPlaceStatisticRepository.Add(newHospitalSectionProfileId);
                _emptyPlaceStatisticRepository.SaveChanges();
            }
            
            var messageText = $"Количество свободных мест для {hospitalSectionProfileName} успешно изменено";

            return new GetChangeNewHospitalRegistrationCommandAnswer
            {
                Token = (Guid)command.Token,
                HasDialogMessage = true,
                DialogMessage = messageText
            };
        }
        public ViewDetailedInformationOnRegisteredPatientsCommandAnswer GetDetailedInformationOnRegisteredPatients(
            ViewDetailedInformationOnRegisteredPatientsCommand command)
        {
            var resrvations = this._reservationRepository.GetModels();

            List<ClinicBreakRegistrationTableItem> table = null;

            if (command.FullInformation == null)
            {
                table = resrvations
                    .Where(model => model.Status == ReservationStatus.Opened)
                    .Where(model => model.EmptyPlaceByTypeStatisticId == command.EmptyPlaceByTypeStatisticId)
                    .Select(model => new ClinicBreakRegistrationTableItem
                    {
                        SectionProfile = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name,
                        ReservationId = model.Id,
                        Haspital =
                            model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Hospital.Name,
                        PatientCode = model.Patient.Code,
                        PatientFirstName = model.Patient.FirstName,
                        PatientLastName = model.Patient.LastName,
                        ReservationDate = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date,
                        Diagnosis = model.Diagnosis
                    })
                    .ToList();
            }
            else
            {
                table = resrvations
                .Where(model => model.Status == ReservationStatus.Opened)
                .Where(model => model.EmptyPlaceByTypeStatisticId == command.EmptyPlaceByTypeStatisticId 
                    || model.EmptyPlaceByTypeStatisticId == command.FullInformation)
                .Select(model => new ClinicBreakRegistrationTableItem
                {
                    SectionProfile = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name,
                    ReservationId = model.Id,
                    Haspital =
                        model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Hospital.Name,
                    PatientCode = model.Patient.Code,
                    PatientFirstName = model.Patient.FirstName,
                    PatientLastName = model.Patient.LastName,
                    ReservationDate = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date,
                    Diagnosis = model.Diagnosis
                })
                .ToList();
            }
            return new ViewDetailedInformationOnRegisteredPatientsCommandAnswer
            {
                Token = (Guid)command.Token,
                Table = table,
                HospitalProfileId = command.HospitalProfileId,
                EmptyPlaceByTypeStatisticId = command.EmptyPlaceByTypeStatisticId,
                FullInformation = command.FullInformation,
                Date = command.Date,
                ShowModalWindow = command.ShowModalWindow
            };
        }

        public BreakHospitalRegistrationCommandAnswer BreakHospitalRegistration(BreakHospitalRegistrationCommand command)
        {
            var reservation = this._reservationRepository.GetModels().FirstOrDefault(model => model.Id == command.ReservationId);
            var patient = this._reservationRepository.GetModels().Where(model => model.Id == command.ReservationId).Select(model => model.Patient).FirstOrDefault();
            var user = this._tokenManager.GetUserByToken(command.Token);

            reservation.CancelTime = DateTime.Now;
            reservation.Status = ReservationStatus.ClosedByHospital;

            this._reservationRepository.Update(command.ReservationId, reservation);

            var hospitalId = this._reservationRepository.GetModels()
                .Where(model => model.Id == command.ReservationId)
                .Select(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId)
                .FirstOrDefault();

            var receiverIds = this._userRepository.GetModels()
                .Where(model => model.HospitalUser != null && model.HospitalUser.HospitalId == hospitalId)
                .Select(model => model.Id)
                .ToList();

            foreach (var receiverId in receiverIds)
            {
                var message = new MessageStorageModel
                {
                    Date = DateTime.Now.Date,
                    IsRead = false,
                    MessageType = MessageType.WarningMessage,
                    ShowStatus = TwoSideShowStatus.Showed,
                    Text = $"Бронирование пациента с номером {patient.Code} был отменено.\0\n" +
                           $"Диагноз: {reservation.Diagnosis}.\n\0",
                    Title = "Уведомление о отмене бронирования места для пациента.",
                    UserFromId = user.Id,
                    UserToId = receiverId
                };

                _messageRepository.Add(message);
            }

            _reservationRepository.SaveChanges();

            this._reservationRepository.SaveChanges();

            return new BreakHospitalRegistrationCommandAnswer
            {
                Token = command.Token.Value
            };
        }

        public GetComingRecordsCommandAnswer GetComingRecords(GetComingRecordsCommand command)
        {   
            var user = _tokenManager.GetUserByToken(command.Token);
            var hospitalId = GetHospitalIdByUserId(user.Id);

            var patientsRepository = _patientRepository.GetModels();
            
             var patients = ((IDbSet<PatientStorageModel>) patientsRepository)
            .Include(model => model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic)
            .Include(model => model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile)
            .Where(model => model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospitalId)
            .Where(model => model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfileId == model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Id)
            .Where(model => model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatisticId == model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Id)
            .Where(model => model.Reservation.EmptyPlaceByTypeStatisticId == model.Reservation.EmptyPlaceByTypeStatistic.Id)
            .Where(model => model.Id == model.Reservation.Id)
            .Where(model => model.Reservation.Status == ReservationStatus.Opened)
            .Select(model => new AllHospitalRegistrations
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Years = model.Years,
                Month = model.Months,
                Weeks = model.Weeks,
                Diagnosis = model.Reservation.Diagnosis,
                SectionName = model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.SectionProfile.Name,
                ClinicName = model.Reservation.Clinic.Name,
                Date = model.Reservation.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date,
                DoctorName = model.Reservation.Reservator.Name,
                RegistrationDate = model.Reservation.ApproveTime,
                ReservationId = model.Reservation.Id
            })
            .OrderBy(registrations => registrations.Date)
            .ThenBy(registrations => registrations.SectionName)
            .ToList();

            List<AllHospitalRegistrations> result = patients.OrderBy(x => x.Date).ToList();

            return new GetComingRecordsCommandAnswer
            {
                Token = command.Token.Value,
                Table =  result
            };
        }

        public ShowAutocompletePageCommandAnswer ShowAutocompletePage(ShowAutocompletePageCommand command)
        {
            var user = this._tokenManager.GetUserByToken(command.Token.Value);
            var hospital = this._hospitalManager.GetHospitalByUser(user);
            var hospitalSectionProfiles =
                this._hospitalSectionProfileRepository.GetModels().Where(model => model.HospitalId == hospital.Id)
                .ToList();

            if (command.HospitalSectionProfileId == null || command.HospitalSectionProfileId == 0)
            {
                command.HospitalSectionProfileId = hospitalSectionProfiles.FirstOrDefault().Id;
            }

            var hasGenderFactor = hospitalSectionProfiles.FirstOrDefault().HasGenderFactor;

            if ((command.SexId == null || command.SexId == 0) && hasGenderFactor)
            {
                command.SexId = (int)Sex.Male;
            }

            var sexes = Enum.GetValues(typeof(Sex))
                .Cast<Sex>()
                .Select(sex => new KeyValuePair<int, string>((int)sex, sex.ToCorrectString()))
                .ToList();

            var hospitalSectionProfilePairs =
                hospitalSectionProfiles.Select(model => new KeyValuePair<int, string>(model.Id, model.Name)).ToList();

            var daysOfWeek = command.DaysOfWeek;
            if (daysOfWeek == null || !daysOfWeek.Any())
            {
                daysOfWeek = @Enum.GetValues(typeof (DayOfWeek)).Cast<DayOfWeek>().Select(week => true).ToList();
            }

            return new ShowAutocompletePageCommandAnswer
            {
                Token = command.Token.Value,
                SexId = command.SexId,
                HospitalSectionProfileId = command.HospitalSectionProfileId.Value,
                Sexes = sexes,
                HospitalSectionProfiles = hospitalSectionProfilePairs,
                HasGenderFactor = hasGenderFactor,
                DaysOfWeek = daysOfWeek,
                HasDialogMessage = command.HasDialogMessage != null && command.HasDialogMessage.Value,
                DialogMessage = command.DialogMessage
            };
        }

        public AutocompleteEmptyPlacesCommandAnswer AutocompleteEmptyPlaces(AutocompleteEmptyPlacesCommand command)
        {
            var user = this._tokenManager.GetUserByToken(command.Token.Value);
            var hospital = this._hospitalManager.GetHospitalByUser(user);
            var section =
                this._hospitalSectionProfileRepository.GetModels()
                    .FirstOrDefault(model => model.Id == command.HospitalSectionProfileId);

            var errors = this.ValidateAutocompleteEmptyPlaces(command);
            if (errors.Any())
            {
                var sexes =
                    Enum.GetValues(typeof(Sex))
                        .Cast<Sex>()
                        .Select(sex => new KeyValuePair<int, string>((int)sex, sex.ToCorrectString()))
                        .ToList();

                var hospitalSectionProfiles =
                    this._hospitalSectionProfileRepository.GetModels()
                        .Where(model => model.HospitalId == hospital.Id)
                        .ToList();

                var hospitalSectionProfilePairs =
                    hospitalSectionProfiles.Select(model => new KeyValuePair<int, string>(model.Id, model.Name))
                        .ToList();

                var hasGenderFactor = hospitalSectionProfiles.FirstOrDefault().HasGenderFactor;

                return new AutocompleteEmptyPlacesCommandAnswer
                           {
                               Token = command.Token.Value,
                               Errors = errors,
                               HospitalSectionProfiles = hospitalSectionProfilePairs,
                               HasGenderFactor = hasGenderFactor,
                               Sexes = sexes,
                               DaysOfWeek = command.DaysOfWeek
                           };
            }

            var placeStatistics = _emptyPlaceByTypeStatisticRepository.GetModels();

            const int ForNextDays = 31; 
            var startDay = DateTime.Now.Date;
            var endDay = startDay.AddDays(ForNextDays);

            var correctPlaceStatistics = ((IDbSet<EmptyPlaceByTypeStatisticStorageModel>)placeStatistics)
                .Include(model => model.EmptyPlaceStatistic)
                .Where(model => model.Sex == (Sex?)command.SexId)
                .Where(model => model.EmptyPlaceStatistic.Date >= startDay && model.EmptyPlaceStatistic.Date <= endDay)
                .Where(model => model.EmptyPlaceStatistic.HospitalSectionProfileId == command.HospitalSectionProfileId)
                .ToList();

            if (hospital.IsForChildren)
            {
                for (var day = 0; day < ForNextDays; day++)
                {
                    var nextDate = startDay.AddDays(day);

                    if (!command.DaysOfWeek[(int)nextDate.Date.DayOfWeek])
                    {
                        continue;
                    }

                    if (!correctPlaceStatistics.Select(model => model.EmptyPlaceStatistic.Date).Contains(nextDate))
                    {
                        var newStatistic = new EmptyPlaceStatisticStorageModel
                                             {
                                                 Date = nextDate,
                                                 HospitalSectionProfileId = command.HospitalSectionProfileId,
                                                 CreateTime = DateTime.Now,
                                                 EmptyPlaceByTypeStatistics = new []
                                                                                  {
                                                                                      new EmptyPlaceByTypeStatisticStorageModel
                                                                                          {
                                                                                              Sex = (Sex?)command.SexId,
                                                                                              Count = command.CountValue
                                                                                          } 
                                                                                  }
                                             };
                        this._emptyPlaceStatisticRepository.Add(newStatistic);
                    }
                }
            }
            else
            {
                throw new NotImplementedException("This function is not implemented for not children's hospitals");
            }

            this._emptyPlaceStatisticRepository.SaveChanges();

            var messageText = $"Автозаполнение свободных дат для отделения *{section.Name}* было успешно выполнено";

            

            return new AutocompleteEmptyPlacesCommandAnswer
                       {
                           Token = command.Token.Value,
                           HasDialogMessage = true,
                           DialogMessage = messageText
                       };
        }

        public SwitchRegistrationPageCommandAnswer SwitchRegistrationPage(SwitchRegistrationPageCommand command)
        {
            return new SwitchRegistrationPageCommandAnswer
            {
                Token = command.Token.Value
            };
        }

        private int GetHospitalEmptyPlacesCount(GetRegistrationScheduleBySectionCommand command, DateTime date, int hospitalId)
        {
           var placeCount = _emptyPlaceByTypeStatisticRepository.GetModels()
               .Where(model => (int)model.Sex == command.SexId 
                   && model.EmptyPlaceStatistic.Date == date
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospitalId
                   && model.EmptyPlaceStatistic.HospitalSectionProfileId == command.HospitalSectionProfileId.Value)
                   .Select(model => model.Count)
                   .FirstOrDefault();

            var registrationCount = _emptyPlaceByTypeStatisticRepository.GetModels()
                .Where(model => (int)model.Sex == command.SexId.Value 
                   && model.EmptyPlaceStatistic.Date == date
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospitalId
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.SectionProfileId == command.HospitalSectionProfileId.Value)
                .SelectMany(model => model.Reservations.Where(storageModel => storageModel.Status == ReservationStatus.Opened))
                .Count(model => model.Status == ReservationStatus.Opened);

            return placeCount - registrationCount;
        }

        public GetRegistrationScheduleBySectionCommandAnswer GetRegistrationScheduleBySection(
            GetRegistrationScheduleBySectionCommand command)
        {
            var user = this._tokenManager.GetUserByToken(command.Token.Value);
            var hospital = this._hospitalManager.GetHospitalByUser(user);
            var hospitalSectionProfiles =
                this._hospitalSectionProfileRepository.GetModels().Where(model => model.HospitalId == hospital.Id)
                .ToList();

            if (command.HospitalSectionProfileId == null || command.HospitalSectionProfileId == 0)
            {
                command.HospitalSectionProfileId = hospitalSectionProfiles.FirstOrDefault().Id;
            }

            if (command.AgeCategoryId == null)
            {
                command.AgeCategoryId = (int)AgeRange.After18;; //Default value for age category = more 1 year
            }

            var hasGenderFactor = hospitalSectionProfiles.FirstOrDefault().HasGenderFactor;
            
            if ((command.SexId == null || command.SexId == 0) && hasGenderFactor)
            {
                command.SexId = (int) Sex.Male;
            }

            const int days = 30;
            var now = DateTime.Now;
            var deadLine = now + new TimeSpan(days, 0, 0, 0);

            var startMonday = GetPreviousMonday(now);
            var endMonday = GetPreviousMonday(deadLine);
            var weeks = (endMonday - startMonday).Days / 7 + 1;

            var startSchedule = Enumerable.Range(0, weeks)
               .Select(week => new ClinicScheduleTableItem
               {
                   Cells = Enumerable.Range(0, 7)
                       .ToDictionary(day => (DayOfWeek)day, day => new ClinicScheduleTableCell
                       {
                           IsBlocked =
                               startMonday.AddDays(7 * week + day).Date < now.Date ||
                               startMonday.AddDays(7 * week + day).Date > deadLine.Date,
                           Day = startMonday.AddDays(7 * week + day).Day,
                           IsThisMonth = startMonday.AddDays(7 * week + day).Month == now.Month,
                           IsThisDate = startMonday.AddDays(7 * week + day).Date == now.Date,
                           Date = startMonday.AddDays(7 * week + day).Date,
                           Count = this.GetHospitalEmptyPlacesCount(command, startMonday.AddDays(7 * week + day).Date, hospital.Id)
                       })
               })
               .ToList();

            var sexes = Enum.GetValues(typeof (Sex))
                .Cast<Sex>()
                .Select(sex => new KeyValuePair<int, string>((int) sex, sex.ToCorrectString()))
                .ToList();

            var hospitalSectionProfilePairs =
                hospitalSectionProfiles.Select(model => new KeyValuePair<int, string>(model.Id, model.Name)).ToList();

            var ageCategories = Enum.GetValues(typeof (AgeRange))
                .Cast<AgeRange>()
                .Select(ageRange => new KeyValuePair<int, string>((int) ageRange, ageRange.ToCorrectString()))
                .ToList();

            return new GetRegistrationScheduleBySectionCommandAnswer
            {
                Token = command.Token.Value,
                SexId = command.SexId,
                HospitalSectionProfileId = command.HospitalSectionProfileId.Value,
                Schedule = startSchedule,
                Sexes = sexes,
                HospitalSectionProfiles = hospitalSectionProfilePairs,
                AgeCategories = ageCategories,
                HasGenderFactor = hasGenderFactor,
                AgeCategoryId = command.AgeCategoryId
            };
            
        }

        private List<CommandAnswerError> ValidateAutocompleteEmptyPlaces(AutocompleteEmptyPlacesCommand command)
        {
            var errors = new List<CommandAnswerError>();

            if (command.CountValue < 0)
            {
                errors.Add(new CommandAnswerError
                               {
                                   FieldName = "Количетсво",
                                   Title = "Количество человек не должно быть меньше нуля"
                               });
            }

            if (command.DaysOfWeek.All(b => b == false))
            {
                errors.Add(new CommandAnswerError
                {
                    FieldName = "Дни",
                    Title = "Выберите хотя бы 1 день"
                });
            }
            return errors;
        }
    }
}
