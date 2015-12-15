using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.Converters;
using ServiceModels.ModelTools.Entities;
using ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers;
using ServiceModels.ServiceCommands.MainMenuCommands;
using Services.Interfaces.MainMenuServices;
using Services.Interfaces.ServiceTools;

namespace Services.MainMenuServices
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IUserFunctionManager _userFunctionManager;
        private readonly IFunctionsNameToMainMenuItemConverter _functionsNameToMainMenuItemConverter;

        public MainMenuService(IUserFunctionManager userFunctionManager, IFunctionsNameToMainMenuItemConverter functionsNameToMainMenuItemConverter)
        {
            _userFunctionManager = userFunctionManager;
            _functionsNameToMainMenuItemConverter = functionsNameToMainMenuItemConverter;
        }

        protected virtual IEnumerable<MainMenuItemValue> ChangeMainMenuItemsByCommand(
            IEnumerable<MainMenuItemValue> mainMenuItemValues, GetMainMenuItemsCommand command)
        {
            // TODO: Implement this method

            var results = mainMenuItemValues.Where(value => value.MainMenuItem == command.ActivatedMainMenu).ToList();

            results.ForEach(value => value.IsActive = true);

            return mainMenuItemValues;
        }

        public GetMainMenuItemsCommandAnswer GetMainMenuItems(GetMainMenuItemsCommand command)
        {
            var userFunctions = _userFunctionManager.GetFunctionsByToken(command.Token);

            var enumList = Enum.GetValues(typeof (MainMenuItem)).Cast<MainMenuItem>().ToList();

            var tabs = userFunctions
                .Select(model => _functionsNameToMainMenuItemConverter.Convert(model.FunctionIdentityName))
                .Where(enumList.Contains)
                .Select(item => new MainMenuItemValue { IsEnabled = true, IsActive = false, MainMenuItem = item })
                .ToList();

            var resultTabs = ChangeMainMenuItemsByCommand(tabs, command).ToList();

            return new GetMainMenuItemsCommandAnswer
            {
                MainMenuTabs = resultTabs
            };
        }
    }
}
