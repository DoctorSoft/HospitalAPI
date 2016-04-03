using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class GetMakeHospitalRegistrationsPageInformationCommand : AbstractMessagedCommand
    {
        public int? HospitalSectionProfileId { get; set; }

        public int? SexId { get; set; }

        public int? AgeCategoryId { get; set; }
    }
}
