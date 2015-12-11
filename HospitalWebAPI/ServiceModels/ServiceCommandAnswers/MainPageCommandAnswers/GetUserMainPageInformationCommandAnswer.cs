using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers
{
    public class GetUserMainPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public UserAccountType UserType { get; set; }
    }
}
