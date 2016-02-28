using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetComingRecordsCommandAnswer: AbstractTokenCommandAnswer
    {
        public List<AllHospitalRegistrations> Table { get; set; }
    }
}
