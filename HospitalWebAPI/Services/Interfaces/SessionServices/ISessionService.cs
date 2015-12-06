using ServiceModels.ServiceCommandAnswers.SessionCommandAnswers;
using ServiceModels.ServiceCommands.SessionCommsnds;

namespace Services.Interfaces.SessionServices
{
    public interface ISessionService
    {
        IsTokenHasAccessToFunctionCommandAnswer IsTokenHasAccessToFunction(IsTokenHasAccessToFunctionCommand command);
    }
}
