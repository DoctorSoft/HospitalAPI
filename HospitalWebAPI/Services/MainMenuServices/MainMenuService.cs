using System.Collections.Generic;
using Enums.Enums;
using ServiceModels.ModelTools.Entities;
using ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers;
using ServiceModels.ServiceCommands.MainMenuCommands;
using Services.Interfaces.MainMenuServices;

namespace Services.MainMenuServices
{
    public class MainMenuService : IMainMenuService
    {
        public GetMainMenuItemsCommandAnswer GetMainMenuItems(GetMainMenuItemsCommand command)
        {
            // TODO: Implement this method

            return new GetMainMenuItemsCommandAnswer
            {
                MainMenuTabs = new List<MainMenuTab>
                {
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = "Home Page"
                    },

                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
                        IsEnabled = true,
                        Label = "Зарегистрировать"
                    },

                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
                        IsEnabled = false,
                        Label = "Неактивное окно"
                    },

                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
                        IsEnabled = true,
                        Label = "Посмотреть сообщения"
                    },
                }
            };
        }
    }
}
