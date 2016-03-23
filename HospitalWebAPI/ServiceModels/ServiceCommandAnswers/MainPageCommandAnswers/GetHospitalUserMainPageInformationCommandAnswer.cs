using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers
{
    public class GetHospitalUserMainPageInformationCommandAnswer : AbstractMessagedCommandAnswer
    {
        public string UserName { get; set; }
        public int? CountNewNotices { get; set; }
        public bool ShowModalWindow { get; set; }
    }
}
