using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Enums.Enums;
using Ninject;
using ServiceModels.ServiceCommands.SessionCommands;
using Services.Interfaces.SessionServices;

namespace HospitalWebAPI.Filters
{
    public class TokenAuthorizationFilter : ActionFilterAttribute
    {
        [Inject]
        public ISessionService SessionService { get; set; }

        private readonly List<FunctionIdentityName> _functions;

        private const string TokenName = "Token";

        public TokenAuthorizationFilter(params FunctionIdentityName[] functions)
        {
            this._functions = functions.ToList();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ActionArguments.Keys.Select(s => s.ToLower()).Contains(TokenName.ToLower()))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var argument = actionContext.ActionArguments[TokenName.ToLower()].ToString();

            var command = new IsTokenHasAccessToFunctionCommand
            {
                Token = Guid.Parse(argument),
                Functions = _functions
            };

            var answer = SessionService.IsTokenHasAccessToFunction(command);

            if (answer.AccessType == AccessType.Denied)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (answer.AccessType == AccessType.Redirected)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}