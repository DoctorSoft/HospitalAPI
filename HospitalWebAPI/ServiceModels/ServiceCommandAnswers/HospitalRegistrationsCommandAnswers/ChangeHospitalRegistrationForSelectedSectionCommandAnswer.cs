using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ChangeHospitalRegistrationForSelectedSectionCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<HospitalRegistrationCountStatisticItem> StatisticItems { get; set; } 
    }
}
