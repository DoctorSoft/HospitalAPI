using ServiceModels.ServiceCommandAnswers.AuthorizationCommandAnswers;
using ServiceModels.ServiceCommands.AuthorizationCommands;
using Services.Interfaces.AuthorizationServices;

namespace Services.AuthorizationServices
{
    public class AuthorizationService : IAuthorizationService
    {
        public GetTokenByUserCredentialsCommandAnswer GetTokenByUserCredentials(GetTokenByUserCredentialsCommand command)
        {
            // TODO: Implement this method
            throw new System.NotImplementedException();
        }
    }
}
