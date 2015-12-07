using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.SessionCommandAnswers;
using ServiceModels.ServiceCommands.SessionCommsnds;
using Services.Interfaces.SessionServices;

namespace Services.SessionServices
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
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

        public bool CheckPresenceOfToken(Guid token)
        {
            var session = _sessionRepository.GetModels().Any(model => model.Token == token);
            return session;
        }

        public IsTokenHasAccessToFunctionCommandAnswer IsTokenHasAccessToFunction(IsTokenHasAccessToFunctionCommand command)
        {
            // TODO: change this module for real functional

            var result = new IsTokenHasAccessToFunctionCommandAnswer
            {
                HasAccess = CheckPresenceOfToken(command.Token)
            };

            if (!result.HasAccess)
            {
                result.Errors = GetAccessDeniedErrors();
            }

            return result;
        }
    }
}
