using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetNewHospitalRegistrationCommandAnswer : AbstractTokenCommandAnswer
    {
        public DateTime Date { get; set; }
    }
}
