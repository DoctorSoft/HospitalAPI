using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Enums.Enums;
using Ninject;
using ServiceModels.ServiceCommands.SessionCommands;
using Services.Interfaces.SessionServices;

namespace HospitalMVC.Filters
{
    public class TokenAuthorizationFilter : ActionFilterAttribute
    {
        [Inject]
        public ISessionService SessionService { get; set; }

        private readonly List<FunctionIdentityName> _functions;

        private const string TokenName = "Token";

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

            var token = Guid.Parse(filterContext.ActionParameters[TokenName].ToString());

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
    }
}