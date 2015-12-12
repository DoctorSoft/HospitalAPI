using System;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.NoticesService;

namespace Services.NoticesService
{
    public class NoticesService : INoticesService
    {
        public GetClinicNoticesPageInformationCommandAnswer GetClinicNoticesPageInformation(
            GetClinicNoticesPageInformationCommand command)
        {
            return new GetClinicNoticesPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }

        public GetHospitalNoticesPageInformationCommandAnswer GetHospitalNoticesPageInformation(
            GetHospitalNoticesPageInformationCommand command)
        {
            return new GetHospitalNoticesPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }
    }
}
