using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ChangeHospitalRegistrationForSelectedSectionCommandAnswer : AbstractTokenCommandAnswer
    {
        public int HospitalProfileId { get; set; }
        public List<HospitalRegistrationCountStatisticItem> StatisticItems { get; set; }
        public string SectionProfileName { get; set; }
        public DateTime Date { get; set; }
        public int CountRegisteredMale { get; set; }
        public int CountRegisteredFemale { get; set; }
    }
}
