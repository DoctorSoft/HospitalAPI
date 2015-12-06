using System.Collections.Generic;
using HelpingTools.Interfaces;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.SessionCommandAnswers;
using ServiceModels.ServiceCommands.SessionCommsnds;
using Services.Interfaces.SessionServices;

namespace Services.SessionServices
{
    public class SessionService : ISessionService
    {
        private readonly IExtendedRandom _extendedRandom;

        public SessionService(IExtendedRandom extendedRandom)
        {
            _extendedRandom = extendedRandom;
        }

        public List<CommandAnswerError> GetAccessDeniedErrors()
        {
            return new List<CommandAnswerError>
            {
                new CommandAnswerError
                {
                    Code = 404,
                    Title = MainResources.ErrorPageIsNotFound_404
                }
            };
        }

        public IsTokenHasAccessToFunctionCommandAnswer IsTokenHasAccessToFunction(IsTokenHasAccessToFunctionCommand command)
        {
            // TODO: change this module for real functional

            var result = new IsTokenHasAccessToFunctionCommandAnswer
            {
                HasAccess = _extendedRandom.GetRandomBool()
            };

            if (!result.HasAccess)
            {
                result.Errors = GetAccessDeniedErrors();
            }

            return result;
        }
    }
}
