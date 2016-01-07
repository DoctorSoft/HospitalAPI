using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class GetClinicMessageByIdCommand : AbstractTokenCommand
    {
        public int MessageId { get; set; }
    }
}
