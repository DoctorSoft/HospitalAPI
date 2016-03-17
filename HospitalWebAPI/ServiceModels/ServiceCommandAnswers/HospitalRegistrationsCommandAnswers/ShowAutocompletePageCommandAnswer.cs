using System.Collections.Generic;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ShowAutocompletePageCommandAnswer : AbstractTokenCommandAnswer
    {
        public int? SexId { get; set; }

        public int? HospitalSectionProfileId { get; set; }

        public List<int> HospitalSectionProfiles { get; set; } 
    }
}
