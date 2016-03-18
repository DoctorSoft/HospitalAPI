namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    using ServiceModels.ModelTools;

    public class AutocompleteEmptyPlacesCommand : AbstractTokenCommand
    {
        public int? SexId { get; set; }

        public int HospitalSectionProfileId { get; set; }

        public int CountValue { get; set; }
    }
}
