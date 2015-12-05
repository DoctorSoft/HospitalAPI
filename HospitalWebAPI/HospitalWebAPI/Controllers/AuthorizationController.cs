using System.Collections.Generic;
using System.Web.Http;
using ServiceModels.ServiceCommandAnswers.AuthorizationCommandAnswers;
using ServiceModels.ServiceCommands.AuthorizationCommands;
using Services.Interfaces.AuthorizationServices;

namespace HospitalWebAPI.Controllers
{
    public class AuthorizationController : ApiController
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        // Action to get authorization token or message of mistake authorization
        // POST api/authorization
        public GetTokenByUserCredentialsCommandAnswer Post([FromBody]GetTokenByUserCredentialsCommand command)
        {
            return _authorizationService.GetTokenByUserCredentials(command);
        }
    }
}