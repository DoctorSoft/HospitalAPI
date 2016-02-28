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

        GetChangeHospitalRegistrationCommandAnswer ApplyChangesHospitalRegistration(
            GetChangeHospitalRegistrationCommand command);

        ChangeHospitalRegistrationForNewSectionCommandAnswer ChangeHospitalRegistrationForNewSection(
            ChangeHospitalRegistrationForNewSectionCommand command);

        GetChangeNewHospitalRegistrationCommandAnswer ApplyChangesNewHospitalRegistration(
            GetChangeNewHospitalRegistrationCommand command);

        ViewDetailedInformationOnRegisteredPatientsCommandAnswer GetDetailedInformationOnRegisteredPatients(
            ViewDetailedInformationOnRegisteredPatientsCommand command);

        BreakHospitalRegistrationCommandAnswer BreakHospitalRegistration(
            BreakHospitalRegistrationCommand command);
        GetComingRecordsCommandAnswer GetComingRecords(
            GetComingRecordsCommand command);
        
    }
}
