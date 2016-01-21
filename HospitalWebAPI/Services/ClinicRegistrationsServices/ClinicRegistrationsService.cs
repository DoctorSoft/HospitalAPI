using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;
using Services.Interfaces.ServiceTools;

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
        
        public ClinicRegistrationsService(ISectionProfileRepository sectionProfileRepository, IClinicManager clinicManager, ITokenManager tokenManager, IEmptyPlaceByTypeStatisticRepository emptyPlaceByTypeStatisticRepository, IClinicHospitalPriorityRepository clinicHospitalPriorityRepository, IHospitalRepository hospitalRepository)
        {
            _sectionProfileRepository = sectionProfileRepository;
            this._clinicManager = clinicManager;
            _tokenManager = tokenManager;
            _emptyPlaceByTypeStatisticRepository = emptyPlaceByTypeStatisticRepository;
            _clinicHospitalPriorityRepository = clinicHospitalPriorityRepository;
            _hospitalRepository = hospitalRepository;
        }

        public GetBreakClinicRegistrationsPageInformationCommandAnswer GetBreakClinicRegistrationsPageInformation(
            GetBreakClinicRegistrationsPageInformationCommand command)
        {
            return new GetBreakClinicRegistrationsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }

        public GetMakeClinicRegistrationsPageInformationCommandAnswer GetMakeClinicRegistrationsPageInformation(
            GetMakeClinicRegistrationsPageInformationCommand command)
        {
            var sexes =
                Enum.GetValues(typeof (Sex))
                    .Cast<Sex>()
                    .Select(sex => new KeyValuePair<int, string>((int) sex, sex.ToString("G")))
                    .ToList();

            var ageSections =
                Enum.GetValues(typeof (AgeSection))
                    .Cast<AgeSection>()
                    .Select(section => new KeyValuePair<int, string>((int) section, section.ToString("G")))
                    .ToList();

            var sectionProfiles =
                _sectionProfileRepository.GetModels()
                    .ToList()
                    .Select(profile => new KeyValuePair<int, string>(profile.Id, profile.Name))
                    .ToList();

            return new GetMakeClinicRegistrationsPageInformationCommandAnswer
            {
                Token = command.Token.Value,
                AgeSections = ageSections,
                AgeSection = ageSections.FirstOrDefault().Value,
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
                Sex = ((Sex)command.Sex).ToString("G"),
                SexId = command.Sex,
                AgeSection = ((AgeSection)command.AgeSection).ToString("G"),
                AgeSectionId = command.AgeSection,
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
            return new GetClinicRegistrationUserFormCommandAnswer
            {
                AgeSectionId = command.AgeSectionId,
                CurrentHospitalId = command.AgeSectionId,
                Date = command.Date,
                SectionProfileId = command.SectionProfileId,
                SexId = command.SexId
            };
        }

        private int GetHospitalEmptyPlacesCount(GetClinicRegistrationScheduleCommand command, DateTime date)
        {
           var placeCount = _emptyPlaceByTypeStatisticRepository.GetModels()
               .Where(model => (int)model.Sex == command.Sex 
                   && (int)model.AgeSection == command.AgeSection 
                   && model.EmptyPlaceStatistic.Date == date
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == command.CurrentHospitalId
                   && model.EmptyPlaceStatistic.HospitalSectionProfile.SectionProfileId == command.SectionProfileId)
                   .Select(model => model.Count)
                   .FirstOrDefault();

            var registrationCount = _emptyPlaceByTypeStatisticRepository.GetModels()
                .Where(model => (int)model.Sex == command.Sex 
                   && (int)model.AgeSection == command.AgeSection 
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
