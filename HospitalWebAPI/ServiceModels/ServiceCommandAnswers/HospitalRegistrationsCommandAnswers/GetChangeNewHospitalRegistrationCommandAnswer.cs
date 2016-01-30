using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetChangeNewHospitalRegistrationCommandAnswer : AbstractTokenCommandAnswer
    {
        public DateTime Date { get; set; }
    }
}
