using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;

namespace Services.ClinicRegistrationsServices
{
    public class ClinicRegistrationsService : IClinicRegistrationsService
    {
        private readonly ISectionProfileRepository _sectionProfileRepository;

        public ClinicRegistrationsService(ISectionProfileRepository sectionProfileRepository)
        {
            _sectionProfileRepository = sectionProfileRepository;
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
                           Count = 0 // todo: implement this
                       })
               })
               .ToList();

            return new GetClinicRegistrationScheduleCommandAnswer
            {
                Token = command.Token.Value,
                Sex = command.Sex,
                AgeSection = command.AgeSection,
                SectionProfileId = command.SectionProfileId,
                Schedule = startSchedule
            };
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
