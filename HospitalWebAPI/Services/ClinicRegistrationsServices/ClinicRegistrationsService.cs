﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Enums.EnumExtensions;
using Enums.Enums;
using HelpingTools.ExtentionTools;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.MailboxModels;

namespace Services.ClinicRegistrationsServices
{
    public class ClinicRegistrationsService : IClinicRegistrationsService
    {
        private readonly ISectionProfileRepository _sectionProfileRepository;

        private readonly IClinicManager _clinicManager;

        private readonly ITokenManager _tokenManager;

        private readonly IEmptyPlaceByTypeStatisticRepository _emptyPlaceByTypeStatisticRepository;

        private readonly IClinicHospitalPriorityRepository _clinicHospitalPriorityRepository;

        private readonly IHospitalRepository _hospitalRepository;

        private readonly IReservationRepository _reservationRepository;

        private readonly IMessageRepository _messageRepository;

        private readonly IUserRepository _userRepository;

        private readonly IHospitalSectionProfileRepository _hospitalSectionProfileRepository;

        private readonly IHospitalManager _hospitalManager;

        private readonly IClinicRepository _clinicRepository;

        public ClinicRegistrationsService(ISectionProfileRepository sectionProfileRepository, IClinicManager clinicManager, ITokenManager tokenManager, IEmptyPlaceByTypeStatisticRepository emptyPlaceByTypeStatisticRepository, IClinicHospitalPriorityRepository clinicHospitalPriorityRepository, IHospitalRepository hospitalRepository, IReservationRepository reservationRepository, IMessageRepository messageRepository, IUserRepository userRepository, IHospitalSectionProfileRepository hospitalSectionProfileRepository, IHospitalManager hospitalManager, IClinicRepository clinicRepository)
        {
            _sectionProfileRepository = sectionProfileRepository;
            this._clinicManager = clinicManager;
            _tokenManager = tokenManager;
            _emptyPlaceByTypeStatisticRepository = emptyPlaceByTypeStatisticRepository;
            _clinicHospitalPriorityRepository = clinicHospitalPriorityRepository;
            _hospitalRepository = hospitalRepository;
            _reservationRepository = reservationRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            this._hospitalSectionProfileRepository = hospitalSectionProfileRepository;
            this._hospitalManager = hospitalManager;
            _clinicRepository = clinicRepository;
        }

        public GetBreakClinicRegistrationsPageInformationCommandAnswer GetBreakClinicRegistrationsPageInformation(
            GetBreakClinicRegistrationsPageInformationCommand command)
        {
            var resrvations = this._reservationRepository.GetModels();
            var now = DateTime.Now.Date;

            var table = resrvations
                .Where(model => model.Status == ReservationStatus.Opened)
                .Where(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date > now)
                .Select(model => new ClinicBreakRegistrationTableItem
                {
                    SectionProfile = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name,
                    ReservationId = model.Id,
                    Haspital = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Hospital.Name,
                    PatientCode = model.Patient.Code,
                    PatientFirstName = model.Patient.FirstName,
                    PatientLastName = model.Patient.LastName,
                    ReservationDate = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date,
                    Diagnosis = model.Diagnosis
                })
                .ToList();

            table.ForEach(item => item.ReservationFormattedDate = item.ReservationDate.ToCorrectDateString());

            return new GetBreakClinicRegistrationsPageInformationCommandAnswer
            {
                Table= table,
                Token = (Guid)command.Token,
                ShowModalWindow = command.ShowModalWindow
            };
        }

        public GetMakeClinicRegistrationsPageInformationCommandAnswer GetMakeClinicRegistrationsPageInformation(
            GetMakeClinicRegistrationsPageInformationCommand command)
        {
            var sexes =
                Enum.GetValues(typeof (Sex))
                    .Cast<Sex>()
                    .Select(sex => new KeyValuePair<int, string>((int) sex, sex.ToCorrectString()))
                    .ToList();

            var sectionProfiles =
                _sectionProfileRepository.GetModels()
                    .ToList()
                    .Select(profile => new KeyValuePair<int, string>(profile.Id, profile.Name))
                    .ToList();

            return new GetMakeClinicRegistrationsPageInformationCommandAnswer
            {
                Token = command.Token.Value,
                SectionProfiles = sectionProfiles,
                SectionProfile = sectionProfiles.FirstOrDefault().Value,
                Sexes = sexes,
                Sex = sexes.FirstOrDefault().Value
            };
        }

        public GetClinicRegistrationScheduleCommandAnswer GetClinicRegistrationSchedule(
            GetClinicRegistrationScheduleCommand command)
        {
            const int days = 30;
            var now = DateTime.Now;
            var deadLine = now + new TimeSpan(days, 0, 0, 0);

            var startMonday = GetPreviousMonday(now);
            var endMonday = GetPreviousMonday(deadLine);
            var weeks = (endMonday - startMonday).Days / 7 + 1;

            var user = this._tokenManager.GetUserByToken(command.Token);
            var clinicId = this._clinicManager.GetClinicByUser(user).Id;

            if (command.CurrentHospitalId == null || command.CurrentHospitalId == 0)
            {
                command.CurrentHospitalId = this.GetDefaultHospitalIdByClinicId(clinicId);
            }

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
                           Count = this.GetHospitalEmptyPlacesCount(command, startMonday.AddDays(7 * week + day).Date)
                       })
               })
               .ToList();

            var hospitals =
                _hospitalRepository.GetModels()
                    .ToList()
                    .Select(profile => new KeyValuePair<int, string>(profile.Id, profile.Name))
                    .ToList();

            return new GetClinicRegistrationScheduleCommandAnswer
            {
                Token = command.Token.Value,
                Sex = ((Sex)command.Sex).ToCorrectString(),
                SexId = command.Sex,
                SectionProfileId = command.SectionProfileId,
                SectionProfile = _sectionProfileRepository
                                    .GetModels()
                                    .FirstOrDefault(model => model.Id == command.SectionProfileId)
                                    .Name,
                Schedule = startSchedule,
                CurrentHospitalId = command.CurrentHospitalId.Value,
                Hospitals = hospitals
            };
        }

        public GetClinicRegistrationUserFormCommandAnswer GetClinicRegistrationUserForm(GetClinicRegistrationUserFormCommand command)
        {
            var hospital =
                this._hospitalRepository.GetModels().FirstOrDefault(model => model.Id == command.CurrentHospitalId);

            var sectionProfile =
                this._sectionProfileRepository.GetModels().FirstOrDefault(model => model.Id == command.SectionProfileId);

            return new GetClinicRegistrationUserFormCommandAnswer
            {
                CurrentHospitalId = command.CurrentHospitalId,
                DateValue = command.Date,
                Date = command.Date.ToCorrectDateString(),
                SectionProfileId = command.SectionProfileId,
                SexId = command.SexId,
                Code = Guid.NewGuid().ToString(),
                CurrentHospital = hospital.Name,
                Sex = ((Sex)command.SexId).ToCorrectString(),
                Token = command.Token.Value,
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                SectionProfile = sectionProfile.Name,
                Age = 0
            };
        }

        private List<CommandAnswerError> ValidateSaveClinicRegistrationCommand(SaveClinicRegistrationCommand command)
        {
            var result = new List<CommandAnswerError>();

            if (string.IsNullOrWhiteSpace(command.FirstName) || command.FirstName.Length < 2)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Имя",
                    Title = "Имя не может иметь меньше 2 букв"
                });
            }

            if (string.IsNullOrWhiteSpace(command.LastName) || command.LastName.Length < 2)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Фамилия",
                    Title = "Фамилия не может иметь меньше 2 букв"
                });
            }

            if (string.IsNullOrWhiteSpace(command.PhoneNumber) || command.PhoneNumber.Length < 3 || !command.PhoneNumber.Any(char.IsDigit))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Телефон",
                    Title = "Телефон имеет некорректный формат"
                });
            }

            if (command.Age < 0)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Возраст не может быть отрицательным"
                });
            }

            if (string.IsNullOrWhiteSpace(command.Diagnosis) || command.Diagnosis.Length < 2)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Диагноз",
                    Title = "Диагноз не может иметь меньше 2 букв"
                });
            }

            return result;
        }
        
        public SaveClinicRegistrationCommandAnswer SaveClinicRegistration(SaveClinicRegistrationCommand command)
        {
            var errors = this.ValidateSaveClinicRegistrationCommand(command);
            if (errors.Any())
            {
                return new SaveClinicRegistrationCommandAnswer
                {
                    Errors = errors, 
                    Token = command.Token.Value
                };
            }

            var user = this._tokenManager.GetUserByToken(command.Token);
            var clinicId = this._clinicManager.GetClinicByUser(user).Id;

            var date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var emptyPlaceByTypeStatistics = _emptyPlaceByTypeStatisticRepository
                .GetModels()
                .Where(model => model.Sex == (Sex)command.SexId 
                    && model.EmptyPlaceStatistic.HospitalSectionProfile.SectionProfileId == command.SectionProfileId
                    && model.EmptyPlaceStatistic.Date == date
                    && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == command.CurrentHospitalId);

            var emptyPlaceByTypeStatisticId = emptyPlaceByTypeStatistics.FirstOrDefault().Id;

            var reservation = new ReservationStorageModel
            {
                Patient = new PatientStorageModel
                {
                    Age = command.Age.Value,
                    Code = command.Code,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    PhoneNumber = command.PhoneNumber,
                    Sex = (Sex) command.SexId
                },
                ApproveTime = DateTime.Now,
                ClinicId = clinicId,
                EmptyPlaceByTypeStatisticId = emptyPlaceByTypeStatisticId,
                Status = ReservationStatus.Opened,
                Diagnosis = command.Diagnosis,
                ReservatorId = user.Id
            };

            _reservationRepository.Add(reservation);

            var receiverIds = this._userRepository.GetModels()
                .Where(model => model.HospitalUser != null && model.HospitalUser.HospitalId == command.CurrentHospitalId)
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
                    Text = $"Пациент с номером {command.Code} был зарезервирован в Вашу больницу.\0\n" +
                           $"Дата: {command.Date}.\n\0" +
                           $"Отделение: {command.SectionProfile}.\n\0" +
                           $"Диагноз: {command.Diagnosis}.\n\0",
                    Title = "Уведомление о бронировании места для пациента.",
                    UserFromId = user.Id,
                    UserToId = receiverId
                };

                _messageRepository.Add(message);
            }

            _reservationRepository.SaveChanges();

            return new SaveClinicRegistrationCommandAnswer
            {
                Token = command.Token.Value
            };
        }

        public BreakClinicRegistrationCommandAnswer BreakClinicRegistration(BreakClinicRegistrationCommand command)
        {
            var reservation = this._reservationRepository.GetModels().FirstOrDefault(model => model.Id == command.ReservationId);
            var patient = this._reservationRepository.GetModels().Where(model => model.Id == command.ReservationId).Select(model => model.Patient).FirstOrDefault();
            var user = this._tokenManager.GetUserByToken(command.Token);

            reservation.CancelTime = DateTime.Now;
            reservation.Status = ReservationStatus.ClosedByClinic;

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
                    Text = $"Бронирование пациента с номером {patient.Code} былj отменено.\0\n" +
                           $"Диагноз: {reservation.Diagnosis}.\n\0",
                    Title = "Уведомление о отмене бронирования места для пациента.",
                    UserFromId = user.Id,
                    UserToId = receiverId
                };

                _messageRepository.Add(message);
            }

            _reservationRepository.SaveChanges();

            this._reservationRepository.SaveChanges();

            return new BreakClinicRegistrationCommandAnswer
            {
                Token = command.Token.Value
            };
        }

        public GetMakeHospitalRegistrationsPageInformationCommandAnswer GetMakeHospitalRegistrationsPageInformation(
            GetMakeHospitalRegistrationsPageInformationCommand command)
        {
            if (command.SexId == null || command.SexId == 0)
            {
                command.SexId = (int) Sex.Male;
            }

            var user = this._tokenManager.GetUserByToken(command.Token.Value);
            var hospital = this._hospitalManager.GetHospitalByUser(user);
            var hospitalSectionProfiles =
                this._hospitalSectionProfileRepository.GetModels().Where(model => model.HospitalId == hospital.Id)
                .ToList();

            if (command.HospitalSectionProfileId == null || command.HospitalSectionProfileId == 0)
            {
                command.HospitalSectionProfileId = hospitalSectionProfiles.FirstOrDefault().Id;
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

            return new GetMakeHospitalRegistrationsPageInformationCommandAnswer
            {
                Token = command.Token.Value,
                SexId = command.SexId.Value,
                HospitalSectionProfileId = command.HospitalSectionProfileId.Value,
                Schedule = startSchedule,
                Sexes = sexes,
                HospitalSectionProfiles = hospitalSectionProfilePairs
            };
        }

        public GetHospitalRegistrationUserFormCommandAnswer GetHospitalRegistrationUserForm(
            GetHospitalRegistrationUserFormCommand command)
        {
            var clinics = this._clinicRepository.GetModels().ToList();
            if (command.ClinicId == null)
            {
                command.ClinicId = clinics.FirstOrDefault().Id;
            }
            var clinicResults = clinics.Select(model => new KeyValuePair<int, string>(model.Id, model.Name)).ToList();

            var users = this._userRepository.GetModels().Where(model => model.ClinicUser != null && model.ClinicUser.ClinicId == command.ClinicId.Value).ToList();
            if (command.UserId == null || users.Select(model => model.Id).All(i => command.UserId.Value != i))
            {
                command.UserId = users.FirstOrDefault().Id;
            }
            var userResults = users.Select(model => new KeyValuePair<int, string>(model.Id, model.Name)).ToList();

            var hospitalSectionProfile =
                this._hospitalSectionProfileRepository.GetModels()
                    .FirstOrDefault(model => model.Id == command.HospitalSectionProfileId)
                    .Name;

            if (command.Code == null)
            {
                command.Code = Guid.NewGuid().ToString();
            }

            return new GetHospitalRegistrationUserFormCommandAnswer
            {
                Token = command.Token.Value,
                SexId = command.SexId,
                HospitalSectionProfileId = command.HospitalSectionProfileId,
                Sex = ((Sex)command.SexId).ToCorrectString(),
                ClinicId = command.ClinicId.Value,
                LastName = command.LastName,
                FirstName = command.FirstName,
                Date = DateTime.Parse(command.Date).ToCorrectDateString(),
                PhoneNumber = command.PhoneNumber,
                Age = command.Age,
                Code = command.Code,
                Diagnosis = command.Diagnosis,
                DoesAgree = command.DoesAgree ?? true,
                UserId = command.UserId.Value,
                Clinics = clinicResults,
                Users = userResults,
                HospitalSectionProfile = hospitalSectionProfile,
            };
        }

        private List<CommandAnswerError> ValidateSaveHospitalRegistrationCommand(SaveHospitalRegistrationCommand command)
        {
            var result = new List<CommandAnswerError>();

            if (string.IsNullOrWhiteSpace(command.FirstName) || command.FirstName.Length < 2)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Имя",
                    Title = "Имя не может иметь меньше 2 букв"
                });
            }

            if (string.IsNullOrWhiteSpace(command.LastName) || command.LastName.Length < 2)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Фамилия",
                    Title = "Фамилия не может иметь меньше 2 букв"
                });
            }

            if (string.IsNullOrWhiteSpace(command.PhoneNumber) || command.PhoneNumber.Length < 3 || !command.PhoneNumber.Any(char.IsDigit))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Телефон",
                    Title = "Телефон имеет некорректный формат"
                });
            }

            if (!command.DoesAgree)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Соглашение",
                    Title = "Вы должны подтвердить, что согласны взять на себя ответственность за регистраию"
                });
            }

            if (command.Age < 0)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Возраст не может быть отрицательным"
                });
            }

            if (string.IsNullOrWhiteSpace(command.Diagnosis) || command.Diagnosis.Length < 2)
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Диагноз",
                    Title = "Диагноз не может иметь меньше 2 букв"
                });
            }

            return result;
        }

        public SaveHospitalRegistrationCommandAnswer SaveHospitalRegistration(SaveHospitalRegistrationCommand command)
        {
            var errors = this.ValidateSaveHospitalRegistrationCommand(command);

            var users = this._userRepository.GetModels().Where(model => model.ClinicUser != null && model.ClinicUser.ClinicId == command.ClinicId).ToList();
            var userResults = users.Select(model => new KeyValuePair<int, string>(model.Id, model.Name)).ToList();
            
            var clinics = this._clinicRepository.GetModels().ToList();
            var clinicResults = clinics.Select(model => new KeyValuePair<int, string>(model.Id, model.Name)).ToList();

            var hospitalSectionProfileName =
                this._hospitalSectionProfileRepository.GetModels()
                    .FirstOrDefault(model => model.Id == command.HospitalSectionProfileId)
                    .Name;

            if (errors.Any())
            {
                return new SaveHospitalRegistrationCommandAnswer
                {
                    SexId = command.SexId,
                    HospitalSectionProfileId = command.HospitalSectionProfileId,
                    Sex = ((Sex) command.SexId).ToCorrectString(),
                    ClinicId = command.ClinicId,
                    LastName = command.LastName,
                    FirstName = command.FirstName,
                    Date = command.Date,
                    PhoneNumber = command.PhoneNumber,
                    Age = command.Age,
                    Code = command.Code,
                    Diagnosis = command.Diagnosis,
                    DoesAgree = command.DoesAgree,
                    UserId = command.UserId,
                    Clinics = clinicResults,
                    Users = userResults,
                    HospitalSectionProfile = hospitalSectionProfileName,
                    Errors = errors,
                    Token = command.Token.Value
                };
            }

            var user = this._tokenManager.GetUserByToken(command.Token);


            var clinicId = command.ClinicId;
            var hospital = this._hospitalManager.GetHospitalByUser(user);

            var date = DateTime.ParseExact(command.Date.Split(' ').First(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var emptyPlaceByTypeStatistics = _emptyPlaceByTypeStatisticRepository
                .GetModels()
                .Where(model => model.Sex == (Sex)command.SexId 
                    && model.EmptyPlaceStatistic.HospitalSectionProfile.Id == command.HospitalSectionProfileId
                    && model.EmptyPlaceStatistic.Date == date
                    && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospital.Id);

            var emptyPlaceByTypeStatisticId = emptyPlaceByTypeStatistics.FirstOrDefault().Id;
            
            var reservation = new ReservationStorageModel
            {
                Patient = new PatientStorageModel
                {
                    Age = command.Age.Value,
                    Code = command.Code,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    PhoneNumber = command.PhoneNumber,
                    Sex = (Sex) command.SexId
                },
                ApproveTime = DateTime.Now,
                ClinicId = clinicId,
                EmptyPlaceByTypeStatisticId = emptyPlaceByTypeStatisticId,
                Status = ReservationStatus.Opened,
                Diagnosis = command.Diagnosis,
                ReservatorId = command.UserId,
                BehalfReservatorId = user.Id
            };

            _reservationRepository.Add(reservation);

            var receiverIds = this._userRepository.GetModels()
                .Where(model => model.HospitalUser != null && model.HospitalUser.HospitalId == hospital.Id)
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
                    Text = $"Пациент с номером {command.Code} был зарезервирован в Вашу больницу.\0\n" +
                           $"Дата: {command.Date}.\n\0" +
                           $"Отделение: {hospitalSectionProfileName}.\n\0" +
                           $"Диагноз: {command.Diagnosis}.\n\0",
                    Title = "Уведомление о бронировании места для пациента.",
                    UserFromId = command.UserId,
                    UserToId = receiverId
                };

                _messageRepository.Add(message);
            }

            _reservationRepository.SaveChanges();
            
            var answer = new SaveHospitalRegistrationCommandAnswer
            {
                Token = command.Token.Value,
                SexId = command.SexId,
                HospitalSectionProfileId = command.HospitalSectionProfileId,
                Sex = ((Sex)command.SexId).ToCorrectString(),
                ClinicId = command.ClinicId,
                LastName = command.LastName,
                FirstName = command.FirstName,
                Date = command.Date,
                PhoneNumber = command.PhoneNumber,
                Age = command.Age,
                Code = command.Code,
                Diagnosis = command.Diagnosis,
                DoesAgree = command.DoesAgree,
                UserId = command.UserId,
                Clinics = clinicResults,
                Users = userResults,
                HospitalSectionProfile = hospitalSectionProfileName,
            };

            return answer;
        }

        private int GetHospitalEmptyPlacesCount(GetClinicRegistrationScheduleCommand command, DateTime date)
        {
           var placeCount = _emptyPlaceByTypeStatisticRepository.GetModels()
               .Where(model => (int)model.Sex == command.Sex 
                   && model.EmptyPlaceStatistic.Date == date
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == command.CurrentHospitalId
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.SectionProfileId == command.SectionProfileId)
                   .Select(model => model.Count)
                   .FirstOrDefault();

            var registrationCount = _emptyPlaceByTypeStatisticRepository.GetModels()
                .Where(model => (int)model.Sex == command.Sex 
                   && model.EmptyPlaceStatistic.Date == date
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == command.CurrentHospitalId
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.SectionProfileId == command.SectionProfileId)
                .SelectMany(model => model.Reservations.Where(storageModel => storageModel.Status == ReservationStatus.Opened))
                .Count(model => model.Status == ReservationStatus.Opened);

            return placeCount - registrationCount;
        }

        private int GetHospitalEmptyPlacesCount(GetMakeHospitalRegistrationsPageInformationCommand command, DateTime date, int hospitalId)
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

        private int GetDefaultHospitalIdByClinicId(int clinicId)
        {
            var result = _clinicHospitalPriorityRepository
                .GetModels()
                .Where(model => model.Priority == 1)
                .FirstOrDefault(model => model.ClinicId == clinicId)
                .HospitalId;

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
    }
}
