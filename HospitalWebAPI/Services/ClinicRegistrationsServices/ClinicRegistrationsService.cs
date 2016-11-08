using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        private readonly IReservationFileRepository _reservationFileRepository;

        public ClinicRegistrationsService(ISectionProfileRepository sectionProfileRepository, IClinicManager clinicManager, ITokenManager tokenManager, IEmptyPlaceByTypeStatisticRepository emptyPlaceByTypeStatisticRepository, IClinicHospitalPriorityRepository clinicHospitalPriorityRepository, IHospitalRepository hospitalRepository, IReservationRepository reservationRepository, IMessageRepository messageRepository, IUserRepository userRepository, IHospitalSectionProfileRepository hospitalSectionProfileRepository, IHospitalManager hospitalManager, IClinicRepository clinicRepository, IReservationFileRepository reservationFile)
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
            _reservationFileRepository = reservationFile;
        }

        public GetBreakClinicRegistrationsPageInformationCommandAnswer GetBreakClinicRegistrationsPageInformation(
            GetBreakClinicRegistrationsPageInformationCommand command)
        {
            
            var user = this._tokenManager.GetUserByToken(command.Token);
            var currentClinicId = this._clinicManager.GetClinicByUser(user).Id;

            var resrvations = this._reservationRepository.GetModels();
            var now = DateTime.Now.Date;

            var table = resrvations
                .Where(model => model.Status == ReservationStatus.Opened)
                .Where(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date >= now)
                .Where(model=>model.ClinicId == currentClinicId)
                .Select(model => new ClinicBreakRegistrationTableItem
                {
                    SectionProfile = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name,
                    ReservationId = model.Id,
                    Haspital = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Hospital.Name,
                    PatientCode = model.Patient.Code,
                    PatientFirstName = model.Patient.FirstName,
                    PatientLastName = model.Patient.LastName,
                    ReservationDate = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date,
                    Diagnosis = model.Diagnosis,
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
            var user = this._tokenManager.GetUserByToken(command.Token);
            var clinic = this._clinicManager.GetClinicByUser(user);
            var hasGenderFactor = !clinic.IsForChildren;
            
            var sexes =
                Enum.GetValues(typeof (Sex))
                    .Cast<Sex>()
                    .Select(sex => new KeyValuePair<int, string>((int) sex, sex.ToCorrectString()))
                    .ToList();

            var hospitalIds = _clinicHospitalPriorityRepository
                .GetModels()
                .Where(model => !model.IsBlocked && model.ClinicId == clinic.Id)
                .Select(model => model.HospitalId)
                .ToList();

            var sectionProfiles =
                _hospitalSectionProfileRepository.GetModels()
                    .Where(model => hospitalIds.Any(id => id == model.HospitalId))
                    .Select(model => new
                    {
                        Id = model.SectionProfileId,
                        Name = model.SectionProfile.Name
                    })
                    .ToList()
                    .Select(profile => new KeyValuePair<int, string>(profile.Id, profile.Name))
                    .Distinct()
                    .ToList();

            return new GetMakeClinicRegistrationsPageInformationCommandAnswer
            {
                Token = command.Token.Value,
                SectionProfiles = sectionProfiles,
                SectionProfile = sectionProfiles.FirstOrDefault().Value,
                Sexes = sexes,
                Sex = sexes.FirstOrDefault().Value,
                HasGenderFactor = hasGenderFactor
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

            if (command.AgeCategoryId == null)
            {
                command.AgeCategoryId = (int)AgeRange.After18;
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

            var hospitals = _clinicHospitalPriorityRepository
                .GetModels()
                .Where(model => !model.IsBlocked && model.ClinicId == clinicId)
                .Select(model => new
                {
                    HospitalId = model.HospitalId,
                    Name = model.Hospital.Name
                })
                .ToList()
                .Select(model => new KeyValuePair<int, string>(model.HospitalId, model.Name))
                .ToList();

            var ageCategories = Enum.GetValues(typeof (AgeRange))
                .Cast<AgeRange>()
                .Select(ageRange => new KeyValuePair<int, string>((int) ageRange, ageRange.ToCorrectString()))
                .ToList();

            return new GetClinicRegistrationScheduleCommandAnswer
            {
                Token = command.Token.Value,
                Sex = command.Sex != null ? ((Sex)command.Sex).ToCorrectString() : string.Empty,
                SexId = command.Sex,
                SectionProfileId = command.SectionProfileId,
                SectionProfile = _sectionProfileRepository
                                    .GetModels()
                                    .FirstOrDefault(model => model.Id == command.SectionProfileId)
                                    .Name,
                Schedule = startSchedule,
                CurrentHospitalId = command.CurrentHospitalId.Value,
                Hospitals = hospitals,
                AgeCategoryId = command.AgeCategoryId,
                AgeCategories = ageCategories,
                HasDialogMessage = command.HasDialogMessage != null && command.HasDialogMessage.Value,
                DialogMessage = command.DialogMessage
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
                Sex = command.SexId != null ? ((Sex)command.SexId).ToCorrectString() : string.Empty,
                Token = command.Token.Value,
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                SectionProfile = sectionProfile.Name,
                Years = 0,
                Months = 0,
                Weeks = 0,
                AgeCategoryId = command.AgeCategoryId
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

            if ((command.Years < 0) &&(command.Years != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Возраст не может быть отрицательным"
                });
            }

            if ((command.Months < 0) &&(command.Months != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Возраст не может быть отрицательным (Месяц)"
                });
            }
            
            if ((command.Months > 12) &&(command.Months != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Количество месяцев не может быть больше 12"
                });
            }
            if ((command.Weeks < 0) &&(command.Weeks != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Возраст не может быть отрицательным (Неделя)"
                });
            }
            if ((command.Weeks > 5) &&(command.Weeks != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Количество недель не может быть больше 5"
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
        
        public static byte[] ReadFully(Stream input)
        {
            input.Position = 0;
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
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
                .Where(model => model.Sex == (Sex?)command.SexId 
                    && model.EmptyPlaceStatistic.HospitalSectionProfile.SectionProfileId == command.SectionProfileId
                    && model.EmptyPlaceStatistic.Date == date
                    && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == command.CurrentHospitalId);

            var emptyPlaceByTypeStatisticId = emptyPlaceByTypeStatistics.FirstOrDefault().Id;

            var reservation = new ReservationStorageModel
            {
                Patient = new PatientStorageModel
                {
                    Years = command.Years == null ? 0 : command.Years.Value,
                    Months = command.Months == null ? 0 : command.Months.Value,
                    Weeks = command.Weeks == null ? 0 : command.Weeks.Value,
                    Code = command.Code,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    PhoneNumber = command.PhoneNumber,
                    Sex = command.SexId == null ? 0 :(Sex) command.SexId
                },
                ApproveTime = DateTime.Now,
                ClinicId = clinicId,
                EmptyPlaceByTypeStatisticId = emptyPlaceByTypeStatisticId,
                Status = ReservationStatus.Opened,
                Diagnosis = command.Diagnosis,
                MedicalExaminationResult = command.MedicalExaminationResult,
                MedicalConsultion = command.MedicalConsultion,
                ReservationPurpose = command.ReservationPurpose,
                OtherInformation = command.OtherInformation,
                ReservatorId = user.Id
            };

            _reservationRepository.Add(reservation);
            
            if (command.File != null)
            {
                var reservationFile = new ReservationFileStorageModel()
                {
                    Name = command.FileName,
                    ReservationId = reservation.Id,
                    Reservation = reservation,
                    File = ReadFully(command.File)
                };

                _reservationFileRepository.Add(reservationFile);
            }

            var receiverIds = this._userRepository.GetModels()
                .Where(model => model.HospitalUser != null && model.HospitalUser.HospitalId == command.CurrentHospitalId)
                .Where(model => model.HospitalUser.HospitalUserSectionAccesses.Any(storageModel => !storageModel.IsBlocked && storageModel.HospitalSectionProfileId == command.SectionProfileId))
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
                    Text = $"Пациент с номером {command.Code} был успешно зарезервирован в Вашу больницу.\0\n" +
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
            
            var messageText = $"Пациент с номером {command.Code} был зарезервирован в Вашу больницу.\0\n" +
                           $"Дата: {command.Date}.\n\0" +
                           $"Отделение: {command.SectionProfile}.\n\0" +
                           $"Диагноз: {command.Diagnosis}.\n\0";


            return new SaveClinicRegistrationCommandAnswer
            {
                Token = command.Token.Value,
                HasDialogMessage = true,
                DialogMessage = messageText
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
            
            var hospitalSectionProfileId = this._reservationRepository.GetModels()
                .Where(model => model.Id == command.ReservationId)
                .Select(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfileId)
                .FirstOrDefault();

            var receiverIds = this._userRepository.GetModels()
                .Where(model => model.HospitalUser != null && model.HospitalUser.HospitalId == hospitalId)
                .Where(model => model.HospitalUser.HospitalUserSectionAccesses.Any(storageModel => !storageModel.IsBlocked && storageModel.HospitalSectionProfileId == hospitalSectionProfileId))
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
            var user = this._tokenManager.GetUserByToken(command.Token.Value);
            var hospital = this._hospitalManager.GetHospitalByUser(user);
            var hospitalSectionProfiles =
                this._hospitalSectionProfileRepository.GetModels().Where(model => model.HospitalId == hospital.Id)
                .ToList();

            if (command.HospitalSectionProfileId == null || command.HospitalSectionProfileId == 0)
            {
                command.HospitalSectionProfileId = hospitalSectionProfiles.FirstOrDefault().Id;
            }

            //if (command.AgeCategoryId == null)
            //{
            //    command.AgeCategoryId = 0; //(int)AgeRange.After18;; //Default value for age category = more 1 year
            //}

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

            return new GetMakeHospitalRegistrationsPageInformationCommandAnswer
            {
                Token = command.Token.Value,
                SexId = command.SexId,
                HospitalSectionProfileId = command.HospitalSectionProfileId.Value,
                Schedule = startSchedule,
                Sexes = sexes,
                HospitalSectionProfiles = hospitalSectionProfilePairs,
                AgeCategories = ageCategories,
                HasGenderFactor = hasGenderFactor,
                AgeCategoryId = command.AgeCategoryId,
                HasDialogMessage = command.HasDialogMessage != null && command.HasDialogMessage.Value,
                DialogMessage = command.DialogMessage
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
                Date = command.Date,////DateTime.Parse(command.Date).ToCorrectDateString(),
                PhoneNumber = command.PhoneNumber,
                Years = command.Years ?? 0,
                Months = command.Months ?? 0,
                Weeks = command.Weeks ?? 0,
                Code = command.Code,
                Diagnosis = command.Diagnosis,
                MedicalExaminationResult = command.MedicalExaminationResult,
                MedicalConsultion = command.MedicalConsultion,
                ReservationPurpose = command.ReservationPurpose,
                OtherInformation = command.OtherInformation,
                DoesAgree = command.DoesAgree ?? true,
                UserId = command.UserId.Value,
                Clinics = clinicResults,
                Users = userResults,
                HospitalSectionProfile = hospitalSectionProfile,
                AgeCategoryId = command.AgeCategoryId
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

            if ((command.Years < 0) &&(command.Years != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Возраст не может быть отрицательным"
                });
            }

            if ((command.Months < 0) &&(command.Months != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Возраст не может быть отрицательным (Месяц)"
                });
            }
            
            if ((command.Months > 12) &&(command.Months != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Количество месяцев не может быть больше 12"
                });
            }
            if ((command.Weeks < 0) &&(command.Weeks != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Возраст не может быть отрицательным (Неделя)"
                });
            }
            if ((command.Weeks > 5) &&(command.Weeks != null))
            {
                result.Add(new CommandAnswerError
                {
                    FieldName = "Возраст",
                    Title = "Количество недель не может быть больше 5"
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
                    Sex = command.SexId == null ? string.Empty : ((Sex) command.SexId).ToCorrectString(),
                    ClinicId = command.ClinicId,
                    LastName = command.LastName,
                    FirstName = command.FirstName,
                    Date = command.Date,
                    PhoneNumber = command.PhoneNumber,
                    Years = command.Years ?? 0,
                    Months = command.Months ?? 0,
                    Weeks = command.Weeks ?? 0,
                    Code = command.Code,
                    Diagnosis = command.Diagnosis,
                    MedicalExaminationResult = command.MedicalExaminationResult,
                    MedicalConsultion = command.MedicalConsultion,
                    ReservationPurpose = command.ReservationPurpose,
                    OtherInformation = command.OtherInformation,
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
                .Where(model => model.Sex == (command.SexId == null || command.SexId == 0 ? (Sex?)null : (Sex?)command.SexId)
                    && model.EmptyPlaceStatistic.HospitalSectionProfile.Id == command.HospitalSectionProfileId
                    && model.EmptyPlaceStatistic.Date == date
                    && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospital.Id);

            var emptyPlaceByTypeStatisticId = emptyPlaceByTypeStatistics.FirstOrDefault().Id;
            
            var reservation = new ReservationStorageModel
            {
                Patient = new PatientStorageModel
                {
                    Years = command.Years == null ? 0 : command.Years.Value,
                    Months = command.Months == null ? 0 : command.Months.Value,
                    Weeks = command.Weeks == null ? 0 : command.Weeks.Value,
                    Code = command.Code,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    PhoneNumber = command.PhoneNumber,
                    Sex = command.SexId == null ? 0 : (Sex) command.SexId
                },
                ApproveTime = DateTime.Now,
                ClinicId = clinicId,
                EmptyPlaceByTypeStatisticId = emptyPlaceByTypeStatisticId,
                Status = ReservationStatus.Opened,
                Diagnosis = command.Diagnosis,
                MedicalExaminationResult = command.MedicalExaminationResult,
                MedicalConsultion = command.MedicalConsultion,
                ReservationPurpose = command.ReservationPurpose,
                OtherInformation = command.OtherInformation,
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
            
            var messageText = "Пациент с номером {command.Code} был успешно зарезервирован в Вашу больницу.\0\n" +
                           $"Дата: {command.Date}.\n\0" +
                           $"Отделение: {hospitalSectionProfileName}.\n\0" +
                           $"Диагноз: {command.Diagnosis}.\n\0";

            var answer = new SaveHospitalRegistrationCommandAnswer
            {
                Token = command.Token.Value,
                SexId = command.SexId,
                HospitalSectionProfileId = command.HospitalSectionProfileId,
                Sex = command.SexId != null ? ((Sex)command.SexId).ToCorrectString() : string.Empty,
                ClinicId = command.ClinicId,
                LastName = command.LastName,
                FirstName = command.FirstName,
                Date = command.Date,
                PhoneNumber = command.PhoneNumber,
                Years = command.Years ?? 0,
                Months = command.Months ?? 0,
                Weeks = command.Weeks ?? 0,
                Code = command.Code,
                Diagnosis = command.Diagnosis,
                MedicalExaminationResult = command.MedicalExaminationResult,
                MedicalConsultion = command.MedicalConsultion,
                ReservationPurpose = command.ReservationPurpose,
                OtherInformation = command.OtherInformation,
                DoesAgree = command.DoesAgree,
                UserId = command.UserId,
                Clinics = clinicResults,
                Users = userResults,
                HospitalSectionProfile = hospitalSectionProfileName,
                HasDialogMessage = true,
                DialogMessage = messageText

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
               .Where(model => (int?)model.Sex == command.SexId 
                   && model.EmptyPlaceStatistic.Date == date
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospitalId
                   && model.EmptyPlaceStatistic.HospitalSectionProfileId == command.HospitalSectionProfileId.Value)
                   .Select(model => model.Count)
                   .FirstOrDefault();

            var registrationCount = _emptyPlaceByTypeStatisticRepository.GetModels()
                .Where(model => (int?)model.Sex == command.SexId
                   && model.EmptyPlaceStatistic.Date == date
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospitalId
                   && model.EmptyPlaceStatistic.HospitalSectionProfileId == command.HospitalSectionProfileId.Value)
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
