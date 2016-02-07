﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Enums.EnumExtensions;
using Enums.Enums;
using HelpingTools.ExtentionTools;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;

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
        
        public ClinicRegistrationsService(ISectionProfileRepository sectionProfileRepository, IClinicManager clinicManager, ITokenManager tokenManager, IEmptyPlaceByTypeStatisticRepository emptyPlaceByTypeStatisticRepository, IClinicHospitalPriorityRepository clinicHospitalPriorityRepository, IHospitalRepository hospitalRepository, IReservationRepository reservationRepository)
        {
            _sectionProfileRepository = sectionProfileRepository;
            this._clinicManager = clinicManager;
            _tokenManager = tokenManager;
            _emptyPlaceByTypeStatisticRepository = emptyPlaceByTypeStatisticRepository;
            _clinicHospitalPriorityRepository = clinicHospitalPriorityRepository;
            _hospitalRepository = hospitalRepository;
            _reservationRepository = reservationRepository;
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
                Token = (Guid)command.Token
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
                Diagnosis = command.Diagnosis
            };

            _reservationRepository.Add(reservation);
            _reservationRepository.SaveChanges();

            return new SaveClinicRegistrationCommandAnswer
            {
                Token = command.Token.Value
            };
        }

        public BreakClinicRegistrationCommandAnswer BreakClinicRegistration(BreakClinicRegistrationCommand command)
        {
            var reservation = this._reservationRepository.GetModels().FirstOrDefault(model => model.Id == command.ReservationId);
            
            reservation.CancelTime = DateTime.Now;
            reservation.Status = ReservationStatus.ClosedByClinic;

            this._reservationRepository.Update(command.ReservationId, reservation);
            this._reservationRepository.SaveChanges();

            return new BreakClinicRegistrationCommandAnswer
            {
                Token = command.Token.Value
            };
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
                .SelectMany(model => model.Reservations)
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

            var dayDifference = Math.Abs((int)dayOfWeek - (int)monday) % 7;
            var previousMonday = date - new TimeSpan(dayDifference, 0, 0, 0);

            return previousMonday;
        }
    }
}
