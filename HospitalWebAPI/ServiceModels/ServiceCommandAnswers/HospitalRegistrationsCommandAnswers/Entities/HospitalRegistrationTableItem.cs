using System.Collections.Generic;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities
{
    public class HospitalRegistrationTableItem
    {
        public int HospitalProfileId { get; set; }

        public string HospitalProfileName { get; set; }

        public List<HospitalRegistrationCountStatisticItem> StatisticItems { get; set; } 
    }
}
