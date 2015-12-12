using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Enums.Enums;
using Ninject;
using ServiceModels.ModelTools;
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

        public TokenAuthorizationFilter(params FunctionIdentityName[] functions)
        {
            this._functions = functions.ToList();
        }

        protected virtual RedirectToRouteResult GetRedirectResult(string controller, string action)
        {
            return new RedirectToRouteResult(new RouteValueDictionary { { "controller", controller }, { "action", action } });
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // TODO: Make refactoring
            if (!filterContext.ActionParameters.ContainsKey(TokenName) || filterContext.ActionParameters[TokenName] == null)
            {
                filterContext.Result = GetRedirectResult(AuthorizationControllerName, AuthorizationAction);
                return;
            }

            var requestCommand = filterContext.ActionParameters[TokenName] as AbstractTokenCommand;

            if (requestCommand == null)
            {
                filterContext.Result = GetRedirectResult(AuthorizationControllerName, AuthorizationAction);
                return;
            }

            var token = requestCommand.Token;

            var command = new IsTokenHasAccessToFunctionCommand
            {
                Functions = _functions.ToList(),
                Token = token
            };

            var answer = SessionService.IsTokenHasAccessToFunction(command);

            if (!answer.HasAccess)
            {
                filterContext.Result = GetRedirectResult(AuthorizationControllerName, AuthorizationAction);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResultBase;
            var viewBag = filterContext.Controller.ViewBag;

            if (viewResult == null)
            {
                viewBag.FunctionList = new List<MainMenuItem>();
                base.OnActionExecuted(filterContext);
            }

            var model = viewResult.Model as AbstractTokenCommandAnswer;

            if (model == null)
            {
                viewBag.FunctionList = new List<MainMenuItem>();
                base.OnActionExecuted(filterContext);
            }

            var result = MainMenuService.GetMainMenuItems(new GetMainMenuItemsCommand
            {
                Token = model.Token
            });
            viewBag.FunctionList = result.MainMenuItems;
            viewBag.DisabledFunctionList = result.DisabledMainMenuItems;
            viewBag.Token = model.Token;

            base.OnActionExecuted(filterContext);
        }
    }
}