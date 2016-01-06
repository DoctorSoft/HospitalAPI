using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetChangeHospitalRegistrationsPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<ScheduleTableItem> Schedule { get; set; }
    }
}
