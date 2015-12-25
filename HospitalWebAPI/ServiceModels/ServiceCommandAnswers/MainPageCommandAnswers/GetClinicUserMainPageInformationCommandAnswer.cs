using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers
{
    public class GetClinicUserMainPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public string UserName { get; set; }
        public int? CountNewNotices { get; set; }
    }
}
