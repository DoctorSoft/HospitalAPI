using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetComingRecordsCommandAnswer: AbstractMessagedCommandAnswer
    {
        public List<AllHospitalRegistrations> Table { get; set; }
    }
}
