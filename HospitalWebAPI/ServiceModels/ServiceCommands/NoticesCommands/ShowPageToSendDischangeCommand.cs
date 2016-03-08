using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class ShowPageToSendDischangeCommand : AbstractTokenCommand
    {
        public int? ClinicId { get; set; }
    }
}
