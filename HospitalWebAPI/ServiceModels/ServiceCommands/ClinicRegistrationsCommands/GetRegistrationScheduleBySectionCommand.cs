using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class GetRegistrationScheduleBySectionCommand : AbstractTokenCommand
    {
        public int? HospitalSectionProfileId { get; set; }

        public int? SexId { get; set; }

        public int? AgeCategoryId { get; set; }
    }
}
