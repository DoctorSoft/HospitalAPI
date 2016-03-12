using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class GetClinicRegistrationScheduleCommand : AbstractTokenCommand
    {
        public int? Sex { get; set; }

        public int SectionProfileId { get; set; }

        public int? CurrentHospitalId { get; set; }
    }
}
