using ServiceModels.ServiceCommandAnswers.AuthorizationCommandAnswers;
using ServiceModels.ServiceCommands.AuthorizationCommands;

namespace Services.Interfaces.AuthorizationServices
{
    public interface IAuthorizationService
    {
        GetTokenByUserCredentialsCommandAnswer GetTokenByUserCredentials(GetTokenByUserCredentialsCommand command);

        LogOutUserByTokenCommandAnswer LogOutUserByToken(LogOutUserByTokenCommand command);
    }
}
