using System.Collections.Generic;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetMakeClinicRegistrationsPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<KeyValuePair<int, string>> SectionProfiles { get; set; }

        public List<KeyValuePair<int, string>> Sexes { get; set; }

        public List<KeyValuePair<int, string>> AgeSections { get; set; }

        public string SectionProfile { get; set; }

        public string Sex { get; set; }

        public string AgeSection { get; set; }
    }
}
