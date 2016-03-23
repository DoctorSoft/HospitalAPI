using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.MainPageCommands
{
    public class GetHospitalUserMainPageInformationCommand : AbstractMessagedCommand
    {
        public bool ShowModalWindow { get; set; }
    }
}
