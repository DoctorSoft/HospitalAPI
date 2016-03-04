using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Dependencies.NinjectFactories;
using Enums.Enums;
using HandleToolsInterfaces.Converters;
using Ninject;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ModelTools.Entities;
using ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers;
using ServiceModels.ServiceCommands.MainMenuCommands;
using ServiceModels.ServiceCommands.SessionCommands;

namespace HospitalMVC.Filters
{
    public class TokenAuthorizationFilter : ActionFilterAttribute
    {
        [Inject]
        public IMainMenuServiceFactory MainMenuServiceFactory { get; set; }

        [Inject]
        public ISessionServiceFactory SessionServiceFactory { get; set; }

        [Inject]
        public IFunctionsNameToMainMenuItemConverter FunctionsNameToMainMenuItemConverter { get; set; }


        private readonly List<FunctionIdentityName> _functions;

        private const string TokenName = "command";

        private const string AuthorizationControllerName = "LogIn";
        private const string AuthorizationActionName = "Index";

        private const string LoginRedirectControllerName = "LogInRedirect";
        private const string LoginRedirectActionName = "RedirectToMainPage";

        private readonly Dictionary<MainMenuItem, MainMenuTab> _mainMenuTabs; 

        public TokenAuthorizationFilter(params FunctionIdentityName[] functions)
        {
            this._functions = functions.ToList();

            _mainMenuTabs = new Dictionary<MainMenuItem, MainMenuTab>()
            {
                {MainMenuItem.HospitalUserSendDistributiveMessagesPage, new MainMenuTab { ActionName = "Index", ControllerName = "HospitalUserSendDistributiveMessagesPage", Label = MainResources.MenuItemSendDistributiveMessagesPage }},
                {MainMenuItem.HospitalUserChangeEmptyPlacesPage, new MainMenuTab { ActionName = "Index", ControllerName = "ChangeHospitalRegistrationPage", Label = MainResources.MenuItemChangeHospitalRegistrations }},
                {MainMenuItem.HospitalUserShowMessagesPage, new MainMenuTab { ActionName = "Index", ControllerName = "HospitalNoticesPage", Label = MainResources.MenuItemViewNotices }},
                {MainMenuItem.HospitalUserMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "HospitalUserHomePage", Label = MainResources.MenuItemHomePage }},
                {MainMenuItem.HospitalUserMakeRegistrationsPage, new MainMenuTab { ActionName = "Index", ControllerName = "MakeHospitalRegistrationPage", Label = "Зарегистрировать" }},
                {MainMenuItem.HospitalUserWatchRegisteredUsers, new MainMenuTab { ActionName = "ShowComingRecords", ControllerName = "ChangeHospitalRegistrationPage", Label = "Показать регистрации"}},

                {MainMenuItem.ClinicUserMakeRegistrationsPage, new MainMenuTab { ActionName = "Index", ControllerName = "MakeClinicRegistrationPage", Label = MainResources.MenuItemMakeClinicRegistration }},
                {MainMenuItem.ClinicUserBreakRegistrationsPage, new MainMenuTab { ActionName = "Index", ControllerName = "BreakClinicRegistrationPage", Label = MainResources.MenuItemBreakClinicRegistration }},
                {MainMenuItem.ClinicUserShowMessagesPage, new MainMenuTab { ActionName = "Index", ControllerName = "ClinicNoticesPage", Label = MainResources.MenuItemViewNotices }},
                {MainMenuItem.ClinicUserMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "ClinicUserHomePage", Label = MainResources.MenuItemHomePage }},

                {MainMenuItem.ReceptionUserMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "ReceptionUserHomePage", Label = MainResources.MenuItemHomePage }},
                {MainMenuItem.ReceptionUserMarkClientsPage, new MainMenuTab { ActionName = "Index", ControllerName = "ReceptionUserMarkClientsPage", Label = MainResources.MenuItemMarkClients }},
                {MainMenuItem.ReceptionUserUnmarkClientsPage, new MainMenuTab { ActionName = "Index", ControllerName = "ReceptionUserUnmarkClientsPage", Label = MainResources.MenuItemsUnmarkClients }},

                {MainMenuItem.AdministratorMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "AdministratorHomePage", Label = MainResources.MenuItemHomePage }},

                {MainMenuItem.ReviewerMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "ReviewerHomePage", Label = MainResources.MenuItemHomePage }},
                {MainMenuItem.ReviewerShowStatisticPage, new MainMenuTab { ActionName = "Index", ControllerName = "ReviewerShowStatisticPage", Label = "Показать статистику" }},

                {MainMenuItem.LogOut, new MainMenuTab { ActionName = "Index", ControllerName = "LogOut", Label = MainResources.MenuItemExit }}
            };
        }

        protected virtual RedirectToRouteResult GetRedirectResult(string controller, string action, string token = "")
        {
            return new RedirectToRouteResult(new RouteValueDictionary { { "controller", controller }, { "action", action }, { "token", token } });
        }

        protected virtual AbstractTokenCommand GetCommandByFilterContext(ActionExecutingContext filterContext)
        {
            if (!filterContext.ActionParameters.ContainsKey(TokenName) || filterContext.ActionParameters[TokenName] == null)
            {
                return null;
            }

            var requestCommand = filterContext.ActionParameters[TokenName] as AbstractTokenCommand;

            return requestCommand;
        }

        protected virtual AccessType GetAccessByCommand(AbstractTokenCommand command)
        {
            if (command == null)
            {
                return AccessType.Denied;
            }

            var token = command.Token;

            var newCommand = new IsTokenHasAccessToFunctionCommand
            {
                Functions = _functions.ToList(),
                Token = token
            };

            var sessionService = SessionServiceFactory.CreateSessionService();
            var answer = sessionService.IsTokenHasAccessToFunction(newCommand);

            return answer.AccessType;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requestCommand = GetCommandByFilterContext(filterContext);
            var accessType = GetAccessByCommand(requestCommand);

            if (accessType == AccessType.Denied)
            {
                filterContext.Result = GetRedirectResult(AuthorizationControllerName, AuthorizationActionName);
                return;
            }

            if (accessType == AccessType.Redirected || accessType == AccessType.Disabled)
            {
                filterContext.Result = GetRedirectResult(LoginRedirectControllerName, LoginRedirectActionName, requestCommand.Token.ToString() );
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        protected virtual AbstractTokenCommandAnswer GetCommandAnswerByFilterContext(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResultBase;

            if (viewResult == null)
            {
                return null;
            }

            var model = viewResult.Model as AbstractTokenCommandAnswer;

            return model;
        }

        protected virtual MainMenuItem GetActivatedMainMenuItem()
        {
            var value = (FunctionIdentityName)_functions.Max(name => (int)name);
            var result = FunctionsNameToMainMenuItemConverter.Convert(value);

            return result;
        }

        protected virtual GetMainMenuItemsCommandAnswer GetMainMenuItemsCommandAnswerByAnswer(AbstractTokenCommandAnswer answer)
        {
            var command = new GetMainMenuItemsCommand
            {
                Token = answer.Token,
                ActivatedMainMenu = GetActivatedMainMenuItem()
            };

            var mainMenuService = MainMenuServiceFactory.CreateMainMenuService();
            var result = mainMenuService.GetMainMenuItems(command);

            return result;
        }

        protected virtual List<MainMenuTab> ConvertToTabList(List<MainMenuItemValue> values)
        {
            var result = new List<MainMenuTab>();

            foreach (var value in values.OrderBy(value => value.MainMenuItem))
            {
                var nextTab = _mainMenuTabs[value.MainMenuItem];
                nextTab.IsActive = value.IsActive;
                nextTab.IsEnabled = value.IsEnabled;

                result.Add(nextTab);
            }

            return result;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewBag = filterContext.Controller.ViewBag;

            var model = GetCommandAnswerByFilterContext(filterContext);

            if (model == null)
            {
                viewBag.MainMenuTabs = new List<MainMenuTab>();
                base.OnActionExecuted(filterContext);
                return;
            }

            var result = GetMainMenuItemsCommandAnswerByAnswer(model);

            viewBag.Token = model.Token;
            viewBag.MainMenuTabs = ConvertToTabList(result.MainMenuTabs);

            base.OnActionExecuted(filterContext);
        }
    }
}