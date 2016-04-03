using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class GetRegistrationScheduleBySectionCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<ClinicScheduleTableItem> Schedule { get; set; }

        public int HospitalSectionProfileId { get; set; }

        public List<KeyValuePair<int, string>> HospitalSectionProfiles { get; set; }

        public int? SexId { get; set; }

        public int? AgeCategoryId { get; set; }

        public List<KeyValuePair<int, string>> Sexes { get; set; }

        public List<KeyValuePair<int, string>> AgeCategories { get; set; }

        public bool HasGenderFactor { get; set; }
    }
}
