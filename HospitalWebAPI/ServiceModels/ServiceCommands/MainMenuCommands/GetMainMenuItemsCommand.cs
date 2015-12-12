using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.MainMenuCommands
{
    public class GetMainMenuItemsCommand : AbstractTokenCommand
    {
        MainMenuItem ActivatedMainMenu { get; set; }
    }
}
