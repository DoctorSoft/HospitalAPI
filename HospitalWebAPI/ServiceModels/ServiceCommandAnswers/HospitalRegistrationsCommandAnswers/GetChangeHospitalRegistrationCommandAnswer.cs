using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetChangeHospitalRegistrationCommandAnswer : AbstractTokenCommandAnswer
    {
        public DateTime Date { get; set; }
    }
}
