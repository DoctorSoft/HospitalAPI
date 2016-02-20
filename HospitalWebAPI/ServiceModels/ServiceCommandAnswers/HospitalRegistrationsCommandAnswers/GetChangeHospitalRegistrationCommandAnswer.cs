using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetChangeHospitalRegistrationCommandAnswer : AbstractTokenCommandAnswer
    {
        public string Date { get; set; }
    }
}
