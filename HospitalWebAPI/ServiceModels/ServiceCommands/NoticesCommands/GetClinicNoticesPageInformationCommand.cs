using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class GetClinicNoticesPageInformationCommand : AbstractTokenCommand
    {
        public bool? OlnyUnRead { get; set; }

        public bool? OnlyToday { get; set; }
    }
}
