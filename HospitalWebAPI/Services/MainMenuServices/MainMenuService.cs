using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private readonly Dictionary<MainMenuItem, MainMenuItemValue> _mainMenuItems;

        public MainMenuService(ITokenManager tokenManager, IUserToAccountTypeConverter userToAccountTypeConverter, IUserTypeRepository userTypeRepository)
        {
            _tokenManager = tokenManager;
            _userToAccountTypeConverter = userToAccountTypeConverter;
            _userTypeRepository = userTypeRepository;

            _mainMenuItems = new Dictionary<MainMenuItem, MainMenuItemValue>
            {
                {MainMenuItem.OpenHospitalRegistrations, new MainMenuItemValue { MainMenuItem = MainMenuItem.OpenHospitalRegistrations}},
                {MainMenuItem.ChangeHospitalRegistrations, new MainMenuItemValue { MainMenuItem = MainMenuItem.ChangeHospitalRegistrations}},
                {MainMenuItem.HospitalNotices, new MainMenuItemValue { MainMenuItem = MainMenuItem.HospitalNotices}},
                {MainMenuItem.HospitalMainPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.HospitalMainPage}},

                {MainMenuItem.MakeClinicRegistration, new MainMenuItemValue { MainMenuItem = MainMenuItem.MakeClinicRegistration}},
                {MainMenuItem.BreakClinicRegistration, new MainMenuItemValue { MainMenuItem = MainMenuItem.BreakClinicRegistration}},
                {MainMenuItem.ClinicNotices, new MainMenuItemValue { MainMenuItem = MainMenuItem.ClinicNotices}},
                {MainMenuItem.ClinicMainPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.ClinicMainPage}},

                {MainMenuItem.AdministratorMainPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.AdministratorMainPage}},

                {MainMenuItem.ReviewerMainPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.ReviewerMainPage}},

                {MainMenuItem.LogOut, new MainMenuItemValue { MainMenuItem = MainMenuItem.LogOut}},
            };
        }

        protected virtual UserType GetUserType(UserStorageModel user)
        {
            var userType = _userTypeRepository.GetModels().FirstOrDefault(model => model.Id == user.UserTypeId);

            return userType.UserType;
        }

        protected virtual List<MainMenuItemValue> GetAdministartorValues()
        {
            var result = new List<MainMenuItemValue>
            {
                _mainMenuItems[MainMenuItem.AdministratorMainPage],
                _mainMenuItems[MainMenuItem.LogOut]
            };

            foreach (var value in result)
            {
                value.IsActive = false;
                value.IsEnabled = true;
            }

            result[0].IsActive = true;

            return result;
        }

        protected virtual List<MainMenuItemValue> GetClinicUserValues()
        {
            var result = new List<MainMenuItemValue>
            {
                _mainMenuItems[MainMenuItem.MakeClinicRegistration],
                _mainMenuItems[MainMenuItem.BreakClinicRegistration],
                _mainMenuItems[MainMenuItem.ClinicMainPage],
                _mainMenuItems[MainMenuItem.ClinicNotices],
                _mainMenuItems[MainMenuItem.LogOut]
            };

            foreach (var value in result)
            {
                value.IsActive = false;
                value.IsEnabled = true;
            }

            result[0].IsActive = true;

            return result;
        }

        protected virtual List<MainMenuItemValue> GetHospitalUserValues()
        {
            var result = new List<MainMenuItemValue>
            {
                _mainMenuItems[MainMenuItem.OpenHospitalRegistrations],
                _mainMenuItems[MainMenuItem.ChangeHospitalRegistrations],
                _mainMenuItems[MainMenuItem.HospitalMainPage],
                _mainMenuItems[MainMenuItem.HospitalNotices],
                _mainMenuItems[MainMenuItem.LogOut]
            };

            foreach (var value in result)
            {
                value.IsActive = false;
                value.IsEnabled = true;
            }

            result[0].IsActive = true;

            return result;
        }

        protected virtual List<MainMenuItemValue> GetReviewerValues()
        {
            var result = new List<MainMenuItemValue>
            {
                _mainMenuItems[MainMenuItem.ReviewerMainPage],
                _mainMenuItems[MainMenuItem.LogOut]
            };

            foreach (var value in result)
            {
                value.IsActive = false;
                value.IsEnabled = true;
            }

            result[0].IsActive = true;

            return result;
        }

        protected virtual List<MainMenuItemValue> GetMenuItemForUser(UserAccountType userAccountType)
        {
            if (userAccountType == UserAccountType.AdministratorAccount)
            {
                return GetAdministartorValues();
            }

            if (userAccountType == UserAccountType.ClinicUserAccount)
            {
                return GetClinicUserValues();
            }

            if (userAccountType == UserAccountType.HospitalUserAccount)
            {
                return GetHospitalUserValues();
            }

            if (userAccountType == UserAccountType.ReviewerAccount)
            {
                return GetReviewerValues();
            }
            /*List<MainMenuTab> listItemsMenu;
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
                        IsActive = false,
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
                        IsActive = false,
                        IsEnabled = true,
                        Label = MainResources.MenuItemMakeClinicRegistration
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
                        IsEnabled = true,
                        Label = MainResources.MenuItemBreakClinicRegistration
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
                        IsEnabled = true,
                        Label = MainResources.MenuItemViewNotices
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
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
                        IsActive = false,
                        IsEnabled = true,
                        Label = MainResources.MenuItemOpenHospitalRegistrations
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
                        IsEnabled = true,
                        Label = MainResources.MenuItemChangeHospitalRegistrations
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
                        IsEnabled = true,
                        Label = MainResources.MenuItemViewNotices
                    },
                    new MainMenuTab
                    {
                        ActionName = "Index",
                        ControllerName = "Home",
                        IsActive = false,
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
                        IsActive = false,
                        IsEnabled = true,
                        Label = MainResources.MenuItemExit
                    }
                };
                return listItemsMenu;
            }*/
            return new List<MainMenuItemValue>();
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
