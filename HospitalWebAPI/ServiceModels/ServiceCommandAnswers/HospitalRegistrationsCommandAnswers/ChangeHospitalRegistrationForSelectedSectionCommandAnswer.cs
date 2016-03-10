using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ChangeHospitalRegistrationForSelectedSectionCommandAnswer : AbstractTokenCommandAnswer
    {
        public int HospitalProfileId { get; set; }
        //public int? EmptyPlaceByTypeStatisticIdMaleId { get; set; }
        //public int? EmptyPlaceByTypeStatisticIdFeMaleId { get; set; }
        public List<HospitalRegistrationCountStatisticItem> StatisticItems { get; set; }
        public string SectionProfileName { get; set; }
        public string Date { get; set; }

        //public int CountRegisteredMale { get; set; }
        //public int CountRegisteredFemale { get; set; }
    }
}
