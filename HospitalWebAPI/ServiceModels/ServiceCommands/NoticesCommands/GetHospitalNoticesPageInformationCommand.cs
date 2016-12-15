using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class GetHospitalNoticesPageInformationCommand : AbstractTokenCommand
    {
        public bool? OlnyUnRead { get; set; }

        public bool? OnlyToday { get; set; }
    }
}
