using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;

namespace Services.Interfaces.MainPageServices
{
    public interface IMainPageService
    {
        GetUserMainPageInformationCommandAnswer GetUserMainPageInformation(GetUserMainPageInformationCommand command);

        GetAdministratorMainPageInformationCommandAnswer GetAdministratorMainPageInformation(
            GetAdministratorMainPageInformationCommand command);

        GetClinicUserMainPageInformationCommandAnswer GetClinicUserMainPageInformation(
            GetClinicUserMainPageInformationCommand command);

        GetHospitalUserMainPageInformationCommandAnswer GetHospitalUserMainPageInformation(
            GetHospitalUserMainPageInformationCommand command);

        GetReviewerMainPageInformationCommandAnswer GetReviewerMainPageInformation(
            GetReviewerMainPageInformationCommand command);

        GetReceptionUserMainPageInformationCommandAnswer GetReceptionUserMainPageInformation(
            GetReceptionUserMainPageInformationCommand command);
    }
}
