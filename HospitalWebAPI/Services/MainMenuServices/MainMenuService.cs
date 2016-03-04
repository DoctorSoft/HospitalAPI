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
            var results = mainMenuItemValues.Where(value => value.MainMenuItem == command.ActivatedMainMenu)
                .OrderBy(value => value.MainMenuItem)
                .ToList();

            results.ForEach(value => value.IsActive = true);

            return mainMenuItemValues;
        }

        protected class AccessItem
        {
            public MainMenuItem MainMenuItem { get; set; }

            public bool IsEnabled { get; set; }
        }

        public GetMainMenuItemsCommandAnswer GetMainMenuItems(GetMainMenuItemsCommand command)
        {
            var userFunctions = _userFunctionManager.GetFunctionsByToken(command.Token);

            var enumList = Enum.GetValues(typeof (MainMenuItem)).Cast<MainMenuItem>().ToList();

            var tabs = userFunctions
                .Select(model => new AccessItem
                {
                    IsEnabled = model.AccessType == AccessType.Accepted,
                    MainMenuItem = _functionsNameToMainMenuItemConverter.Convert(model.FunctionStorageModel.FunctionIdentityName)
                })
                .Where(item => enumList.Contains(item.MainMenuItem))
                .Select(item => new MainMenuItemValue { IsEnabled = item.IsEnabled, IsActive = false, MainMenuItem = item.MainMenuItem })
                .ToList();

            var resultTabs = ChangeMainMenuItemsByCommand(tabs, command).ToList();

            return new GetMainMenuItemsCommandAnswer
            {
                MainMenuTabs = resultTabs
            };
        }
    }
}
