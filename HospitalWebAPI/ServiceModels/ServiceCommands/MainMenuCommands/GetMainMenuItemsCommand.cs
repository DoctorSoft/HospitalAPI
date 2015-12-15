using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.MainMenuCommands
{
    public class GetMainMenuItemsCommand : AbstractTokenCommand
    {
        public MainMenuItem ActivatedMainMenu { get; set; }
    }
}
