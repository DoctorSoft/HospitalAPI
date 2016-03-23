namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    using System.Collections.Generic;

    using ServiceModels.ModelTools;
    public class AutocompleteEmptyPlacesCommandAnswer : AbstractMessagedCommandAnswer
    {
        public List<KeyValuePair<int, string>> HospitalSectionProfiles { get; set; }

        public List<KeyValuePair<int, string>> Sexes { get; set; }

        public bool HasGenderFactor { get; set; }
    }
}
