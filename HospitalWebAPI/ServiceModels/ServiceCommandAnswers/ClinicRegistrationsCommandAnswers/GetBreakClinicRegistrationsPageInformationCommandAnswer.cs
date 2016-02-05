using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetBreakClinicRegistrationsPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<ClinicBreakRegistrationTableItem> Table { get; set; }
    }
}
