using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetMakeHospitalRegistrationsPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<ClinicScheduleTableItem> Schedule { get; set; }

        public int HospitalSectionProfileId { get; set; }

        public List<KeyValuePair<int, string>> HospitalSectionProfiles { get; set; }

        public int? SexId { get; set; }

        public List<KeyValuePair<int, string>> Sexes { get; set; }

        public bool HasGenderFactor { get; set; }
    }
}
