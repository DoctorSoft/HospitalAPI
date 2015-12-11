using ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers;
using ServiceModels.ServiceCommands.MainMenuCommands;

namespace Services.Interfaces.MainMenuServices
{
    public interface IMainMenuService
    {
        GetMainMenuItemsCommandAnswer GetMainMenuItems(GetMainMenuItemsCommand command);
    }
}
