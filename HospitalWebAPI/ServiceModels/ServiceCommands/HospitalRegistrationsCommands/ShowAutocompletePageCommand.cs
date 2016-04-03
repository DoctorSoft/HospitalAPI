using System.Collections.Generic;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ShowAutocompletePageCommand : AbstractMessagedCommand
    {
        public int? HospitalSectionProfileId { get; set; }

        public int? SexId { get; set; }

        public List<bool> DaysOfWeek { get; set; } 
    }
}
