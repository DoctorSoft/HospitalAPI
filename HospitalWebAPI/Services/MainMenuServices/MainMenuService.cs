using System.Collections.Generic;
using Enums.Enums;
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
                MainMenuItems = new List<MainMenuItem>
                {
                    MainMenuItem.ShowHospitalNotices,
                    MainMenuItem.ShowClinicNotices,
                    MainMenuItem.OpenHospitalRegistrations,
                    MainMenuItem.MakeClinicRegistration,
                    MainMenuItem.ChangeHospitalRegistrations,
                    MainMenuItem.BreakClinicRegistration
                }
            };
        }
    }
}
