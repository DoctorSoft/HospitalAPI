using System.Collections.Generic;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ShowAutocompletePageCommandAnswer : AbstractTokenCommandAnswer
    {
        public int HospitalSectionProfileId { get; set; }

        public List<KeyValuePair<int, string>> HospitalSectionProfiles { get; set; }

        public int? SexId { get; set; }

        public List<KeyValuePair<int, string>> Sexes { get; set; }

        public bool HasGenderFactor { get; set; }

        public int CountValue { get; set; }
    
        public List<bool> DaysOfWeek{get; set; } 
    }
}
