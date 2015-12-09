using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers
{
    public class GetUserMainPageInformationCommandAnswer : AbstractCommandAnswer
    {
        public UserAccountType UserType { get; set; }
    }
}
