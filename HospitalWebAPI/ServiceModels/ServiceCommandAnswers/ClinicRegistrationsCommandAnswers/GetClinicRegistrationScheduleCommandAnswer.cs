using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetClinicRegistrationScheduleCommandAnswer : AbstractTokenCommandAnswer
    {
        public string Sex { get; set; }

        public string AgeSection { get; set; }

        public string SectionProfileId { get; set; }

        public List<ClinicScheduleTableItem> Schedule { get; set; }

        public int CurrentHospitalId { get; set; }

        public List<KeyValuePair<int, string>> Hospitals { get; set; }
    }
}
