using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.Converters;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using ServiceModels.ModelTools.Entities;
using ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers;
using ServiceModels.ServiceCommands.MainMenuCommands;
using Services.Interfaces.MainMenuServices;
using Services.Interfaces.ServiceTools;
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
                {MainMenuItem.ClinicUserMainPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.ClinicUserMainPage}},
                {MainMenuItem.ClinicUserMakeRegistrationsPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.ClinicUserMakeRegistrationsPage}},
                {MainMenuItem.ClinicUserBreakRegistrationsPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.ClinicUserBreakRegistrationsPage}},
                {MainMenuItem.ClinicUserShowMessagesPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.ClinicUserShowMessagesPage}},

                {MainMenuItem.HospitalUserMainPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.HospitalUserMainPage}},
                {MainMenuItem.HospitalUserFillEmptyPlacesPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.HospitalUserFillEmptyPlacesPage}},
                {MainMenuItem.HospitalUserChangeEmptyPlacesPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.HospitalUserChangeEmptyPlacesPage}},
                {MainMenuItem.HospitalUserShowMessagesPage, new MainMenuItemValue { MainMenuItem = MainMenuItem.HospitalUserShowMessagesPage}},

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

        protected virtual IEnumerable<MainMenuItemValue> GetFunctionsByDiapason(int minValue, int maxValue)
        {
            var results =
                Enum.GetValues(typeof(MainMenuItem))
                    .Cast<MainMenuItem>()
                    .Where(name => ((int)name) >= minValue && ((int)name) < maxValue)
                    .ToList();

            results.Add(MainMenuItem.LogOut);

            return results.Select(item => _mainMenuItems[item]).ToList();
        }

        protected virtual List<MainMenuItemValue> GetClinicUserValues()
        {
            var result = GetFunctionsByDiapason(200, 300).ToList();

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
            var result = GetFunctionsByDiapason(100, 200).ToList();

            foreach (var value in result)
            {
                value.IsActive = false;
                value.IsEnabled = true;
            }

            result[0].IsActive = true;

            return result;
        }

        protected virtual List<MainMenuItemValue> GetAdministartorValues()
        {
            var result = GetFunctionsByDiapason(400, 500).ToList();

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
            var result = GetFunctionsByDiapason(500, 600).ToList();

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
