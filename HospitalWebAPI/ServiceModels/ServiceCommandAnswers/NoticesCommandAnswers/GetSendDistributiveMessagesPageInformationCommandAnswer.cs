using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers
{
    public class GetSendDistributiveMessagesPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
