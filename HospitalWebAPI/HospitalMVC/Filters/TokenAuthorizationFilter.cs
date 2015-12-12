using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Enums.Enums;
using Ninject;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ModelTools.Entities;
using ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers;
using ServiceModels.ServiceCommands.MainMenuCommands;
using ServiceModels.ServiceCommands.SessionCommands;
using Services.Interfaces.MainMenuServices;
using Services.Interfaces.SessionServices;

namespace HospitalMVC.Filters
{
    public class TokenAuthorizationFilter : ActionFilterAttribute
    {
        [Inject]
        public ISessionService SessionService { get; set; }

        [Inject]
        public IMainMenuService MainMenuService { get; set; }


        private readonly List<FunctionIdentityName> _functions;

        private const string TokenName = "command";

        private const string AuthorizationControllerName = "LogIn";

        private const string AuthorizationAction = "Index";

        private readonly Dictionary<MainMenuItem, MainMenuTab> _mainMenuTabs; 

        public TokenAuthorizationFilter(params FunctionIdentityName[] functions)
        {
            this._functions = functions.ToList();

            _mainMenuTabs = new Dictionary<MainMenuItem, MainMenuTab>()
            {
                {MainMenuItem.OpenHospitalRegistrations, new MainMenuTab { ActionName = "Index", ControllerName = "HospitalRegistrationsPage", Label = MainResources.MenuItemOpenHospitalRegistrations }},
                {MainMenuItem.ChangeHospitalRegistrations, new MainMenuTab { ActionName = "Index", ControllerName = "ChangeHospitalRegistrationPage", Label = MainResources.MenuItemChangeHospitalRegistrations }},
                {MainMenuItem.HospitalNotices, new MainMenuTab { ActionName = "Index", ControllerName = "HospitalNoticesPage", Label = MainResources.MenuItemViewNotices }},
                {MainMenuItem.HospitalMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "HospitalUserHomePage", Label = MainResources.MenuItemHomePage }},

                {MainMenuItem.MakeClinicRegistration, new MainMenuTab { ActionName = "Index", ControllerName = "MakeClinicRegistrationPage", Label = MainResources.MenuItemBreakClinicRegistration }},
                {MainMenuItem.BreakClinicRegistration, new MainMenuTab { ActionName = "Index", ControllerName = "BreakClinicRegistrationPage", Label = MainResources.MenuItemBreakClinicRegistration }},
                {MainMenuItem.ClinicNotices, new MainMenuTab { ActionName = "Index", ControllerName = "ClinicNoticesPage", Label = MainResources.MenuItemViewNotices }},
                {MainMenuItem.ClinicMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "ClinicUserHomePage", Label = MainResources.MenuItemHomePage }},

                {MainMenuItem.AdministratorMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "AdministratorHomePage", Label = MainResources.MenuItemHomePage }},

                {MainMenuItem.ReviewerMainPage, new MainMenuTab { ActionName = "Index", ControllerName = "ReviewerHomePage", Label = MainResources.MenuItemHomePage }},

                {MainMenuItem.LogOut, new MainMenuTab { ActionName = "Index", ControllerName = "LigIn", Label = MainResources.MenuItemExit }},
            };
        }

        protected virtual RedirectToRouteResult GetRedirectResult(string controller, string action)
        {
            return new RedirectToRouteResult(new RouteValueDictionary { { "controller", controller }, { "action", action } });
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

        protected virtual bool GetAccessByCommand(AbstractTokenCommand command)
        {
            var token = command.Token;

            var newCommand = new IsTokenHasAccessToFunctionCommand
            {
                Functions = _functions.ToList(),
                Token = token
            };

            var answer = SessionService.IsTokenHasAccessToFunction(newCommand);

            return answer.HasAccess;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requestCommand = GetCommandByFilterContext(filterContext);

            if (requestCommand == null || !GetAccessByCommand(requestCommand))
            {
                filterContext.Result = GetRedirectResult(AuthorizationControllerName, AuthorizationAction);
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

        protected virtual GetMainMenuItemsCommandAnswer GetMainMenuItemsCommandAnswerByAnswer(AbstractTokenCommandAnswer answer)
        {
            var command = new GetMainMenuItemsCommand
            {
                Token = answer.Token
            };

            var result = MainMenuService.GetMainMenuItems(command);

            return result;
        }

        protected virtual List<MainMenuTab> ConvertToTabList(List<MainMenuItemValue> values)
        {
            var result = new List<MainMenuTab>();

            foreach (var value in values)
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
            }

            var result = GetMainMenuItemsCommandAnswerByAnswer(model);

            viewBag.Token = model.Token;
            viewBag.MainMenuTabs = ConvertToTabList(result.MainMenuTabs);

            base.OnActionExecuted(filterContext);
        }
    }
}