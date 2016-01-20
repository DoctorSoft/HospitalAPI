using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;

namespace Services.Interfaces.HospitalRegistrationsService
{
    public interface IHospitalRegistrationsService
    {
        GetChangeHospitalRegistrationsPageInformationCommandAnswer GetChangeHospitalRegistrationsPageInformation(
            GetChangeHospitalRegistrationsPageInformationCommand command);

        ShowHospitalRegistrationPlacesByDateCommandAnswer ShowHospitalRegistrationPlacesByDate(
            ShowHospitalRegistrationPlacesByDateCommand command);

        ChangeHospitalRegistrationForSelectedSectionCommandAnswer ChangeHospitalRegistrationForSelectedSection(
            ChangeHospitalRegistrationForSelectedSectionCommand command);
    }
}
