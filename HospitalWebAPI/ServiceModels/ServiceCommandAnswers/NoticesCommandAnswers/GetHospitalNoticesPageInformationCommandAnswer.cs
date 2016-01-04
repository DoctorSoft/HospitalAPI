using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers
{
    public class GetHospitalNoticesPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<MessageTableItem> Messages { get; set; } 
    }
}
