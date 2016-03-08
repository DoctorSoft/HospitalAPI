using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class DownloadDischargeCommand : AbstractTokenCommand
    {
        public int DischargeId { get; set; }
    }
}
