using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers;
using ServiceModels.ServiceCommands.NoticesCommands;

namespace Services.Interfaces.NoticesService
{
    public interface INoticesService
    {
        GetClinicNoticesPageInformationCommandAnswer GetClinicNoticesPageInformation(
            GetClinicNoticesPageInformationCommand command);

        GetHospitalNoticesPageInformationCommandAnswer GetHospitalNoticesPageInformation(
            GetHospitalNoticesPageInformationCommand command);
    }
}
