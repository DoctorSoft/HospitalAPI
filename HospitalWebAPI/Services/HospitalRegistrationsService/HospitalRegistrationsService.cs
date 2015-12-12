using System;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;
using Services.Interfaces.HospitalRegistrationsService;

namespace Services.HospitalRegistrationsService
{
    public class HospitalRegistrationsService : IHospitalRegistrationsService
    {
        public GetChangeHospitalRegistrationsPageInformationCommandAnswer GetChangeHospitalRegistrationsPageInformation(
            GetChangeHospitalRegistrationsPageInformationCommand command)
        {
            return  new GetChangeHospitalRegistrationsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }

        public GetOpenHospitalRegistrationsPageInformationCommandAnswer GetOpenHospitalRegistrationsPageInformation(
            GetOpenHospitalRegistrationsPageInformationCommand command)
        {
            return new GetOpenHospitalRegistrationsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }
    }
}
