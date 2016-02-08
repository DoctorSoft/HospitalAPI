using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class GetMakeHospitalRegistrationsPageInformationCommand : AbstractTokenCommand
    {
        public int? HospitalSectionProfileId { get; set; }

        public int? SexId { get; set; }
    }
}
