using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ShowAutocompletePageCommand : AbstractTokenCommand
    {
        public int? HospitalSectionProfileId { get; set; }

        public int? SexId { get; set; }
    }
}
