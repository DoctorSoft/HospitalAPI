using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class RemoveHospitalMessageByIdCommand : AbstractTokenCommand
    {
        public int MessageId { get; set; }
    }
}
