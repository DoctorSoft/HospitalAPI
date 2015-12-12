using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.Converters;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using Resources;
using ServiceModels.ModelTools.Entities;
using ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers;
using ServiceModels.ServiceCommands.MainMenuCommands;
using Services.Interfaces.MainMenuServices;
using Services.Interfaces.ServiceTools;
using Services.ServiceTools;
using StorageModels.Models.UserModels;

namespace Services.MainMenuServices
{
    public class MainMenuService : IMainMenuService
    {
        private readonly ITokenManager _tokenManager;
        private readonly IUserToAccountTypeConverter _userToAccountTypeConverter;
        private readonly IUserTypeRepository _userTypeRepository;

        public MainMenuService(ITokenManager tokenManager, IUserToAccountTypeConverter userToAccountTypeConverter, IUserTypeRepository userTypeRepository)
        {
            _tokenManager = tokenManager;
            _userToAccountTypeConverter = userToAccountTypeConverter;
            _userTypeRepository = userTypeRepository;
        }

        protected virtual UserType GetUserType(UserStorageModel user)
        {
            var userType = _userTypeRepository.GetModels().FirstOrDefault(model => model.Id == user.UserTypeId);

            return userType.UserType;
        }

        protected virtual List<MainMenuTab> GetMenuItemForUser(UserAccountType userAccountType)
        {
            List<MainMenuTab> listItemsMenu;
            if (userAccountType.Equals(UserAccountType.AdministratorAccount))
            {
                listItemsMenu = new List<MainMenuTab>
                {
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemHomePage
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemExit
                    }
                };
                return listItemsMenu;
            }
            else if (userAccountType.Equals(UserAccountType.ClinicUserAccount))
            {
                listItemsMenu = new List<MainMenuTab>
                {
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemHomePage
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemMakeClinicRegistration
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemBreakClinicRegistration
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemViewNotices
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemExit
                    }
                };
                return listItemsMenu;
            } else if (userAccountType.Equals(UserAccountType.HospitalUserAccount))
            {
                listItemsMenu = new List<MainMenuTab>
                {
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemHomePage
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemOpenHospitalRegistrations
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemChangeHospitalRegistrations
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemViewNotices
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemExit
                    }
                };
                return listItemsMenu;
            }
            else if (userAccountType.Equals(UserAccountType.ReviewerAccount))
            {
                listItemsMenu = new List<MainMenuTab>
                {
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemHomePage
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = true,
                        IsEnabled = true,
                        Label = MainResources.MenuItemExit
                    }
                };
                return listItemsMenu;
            }
            return null;
        }

        public GetMainMenuItemsCommandAnswer GetMainMenuItems(GetMainMenuItemsCommand command)
        {
            // TODO: Implement this method
            var user = _tokenManager.GetUserByToken(command.Token);
            var userType = GetUserType(user);
            var accountType = _userToAccountTypeConverter.Convert(userType);

            return new GetMainMenuItemsCommandAnswer
            {
                MainMenuTabs = GetMenuItemForUser(accountType)
            };
        }
    }
}
