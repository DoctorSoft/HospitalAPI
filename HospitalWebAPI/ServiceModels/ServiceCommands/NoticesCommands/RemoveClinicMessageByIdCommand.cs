using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class RemoveClinicMessageByIdCommand: AbstractTokenCommand
    {
        public int MessageId { get; set; }
    }
}
