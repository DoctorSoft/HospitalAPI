using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetChangeHospitalRegistrationCommandAnswer : AbstractMessagedCommandAnswer
    {
        public string Date { get; set; }
    }
}
