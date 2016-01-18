using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
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
            return new GetClinicRegistrationScheduleCommandAnswer
            {
                Token = command.Token.Value
            };
        }
    }
}
