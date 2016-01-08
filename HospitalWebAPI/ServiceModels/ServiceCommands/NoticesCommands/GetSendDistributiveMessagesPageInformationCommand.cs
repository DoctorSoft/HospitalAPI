using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class GetSendDistributiveMessagesPageInformationCommand : AbstractTokenCommand
    {
        public string Text { get; set; }

        public string Title { get; set; }
    }
}
