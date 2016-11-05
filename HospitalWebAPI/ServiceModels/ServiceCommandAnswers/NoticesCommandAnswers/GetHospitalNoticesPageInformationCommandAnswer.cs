using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers
{
    public class GetHospitalNoticesPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public bool? OlnyUnRead { get; set; }

        public bool? OnlyToday { get; set; }

        public List<MessageTableItem> Messages { get; set; }
    }
}
