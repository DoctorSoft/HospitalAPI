using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;

namespace Services.Interfaces.ClinicRegistrationsServices
{
    public interface IClinicRegistrationsService
    {
        GetBreakClinicRegistrationsPageInformationCommandAnswer GetBreakClinicRegistrationsPageInformation(
            GetBreakClinicRegistrationsPageInformationCommand command);

        GetMakeClinicRegistrationsPageInformationCommandAnswer GetMakeClinicRegistrationsPageInformation(
            GetMakeClinicRegistrationsPageInformationCommand command);

        GetClinicRegistrationScheduleCommandAnswer GetClinicRegistrationSchedule(
            GetClinicRegistrationScheduleCommand command);

        GetClinicRegistrationUserFormCommandAnswer GetClinicRegistrationUserForm(
            GetClinicRegistrationUserFormCommand command);

        SaveClinicRegistrationCommandAnswer SaveClinicRegistration(SaveClinicRegistrationCommand command);

        BreakClinicRegistrationCommandAnswer BreakClinicRegistration(BreakClinicRegistrationCommand command);

        GetMakeHospitalRegistrationsPageInformationCommandAnswer GetMakeHospitalRegistrationsPageInformation(
            GetMakeHospitalRegistrationsPageInformationCommand command);
    }
}
