using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class GetHospitalMessageByIdCommand : AbstractTokenCommand
    {
        public int MessageId { get; set; }
    }
}
