using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ChangeHospitalRegistrationForNewSectionCommand : AbstractTokenCommand
    {
        public string Date { get; set; }
    }
}
