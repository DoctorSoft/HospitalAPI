using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.SessionCommandAnswers;
using ServiceModels.ServiceCommands.SessionCommands;
using Services.Interfaces.SessionServices;
using StorageModels.Models.FunctionModels;

namespace Services.SessionServices
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserFunctionRepository _userFunctionRepository;
        private readonly IFunctionRepository _functionRepository;

        public SessionService(ISessionRepository sessionRepository, IUserFunctionRepository userFunctionRepository, IFunctionRepository functionRepository)
        {
            _sessionRepository = sessionRepository;
            _userFunctionRepository = userFunctionRepository;
            _functionRepository = functionRepository;
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
        private class UserFunctioSelectorModel
        {
            public int UserTypeId { get; set; }

            public IEnumerable<GroupFunctionStorageModel> GroupFunctions { get; set; }
        }

        public bool CheckPresenceOfToken(IsTokenHasAccessToFunctionCommand command)
        {
            var currentSession = _sessionRepository.GetModels().FirstOrDefault(model => model.Token == command.Token);
            List<FunctionStorageModel> functionInTable = new List<FunctionStorageModel>();

            if (currentSession != null && currentSession.IsBlocked)
            {
                return false; //current session is blocked
            }
       
            var functionSelectors = ((IDbSet<UserFunctionStorageModel>)_userFunctionRepository.GetModels())
            .Where(model => model.UserId == currentSession.AccountId)
            .Select(model => model).ToList();

            foreach (var currentFunctionInTable in command.Functions)
            {
                functionInTable = ((IDbSet<FunctionStorageModel>) _functionRepository.GetModels())
                    .Where(model => model.FunctionIdentityName == currentFunctionInTable)
                    .Select(model => model).ToList();
            }

            var result = functionSelectors.Where(n => functionInTable.Any(t => t.Id == n.FunctionId && !t.IsBlocked)).ToList();
            if (result.Count != command.Functions.Count)
            {
                return false;
            }

            return true;
        }

        public IsTokenHasAccessToFunctionCommandAnswer IsTokenHasAccessToFunction(IsTokenHasAccessToFunctionCommand command)
        {
            // TODO: change this module for real functional

            var result = new IsTokenHasAccessToFunctionCommandAnswer
            {
                HasAccess = CheckPresenceOfToken(command)
            };

            if (!result.HasAccess)
            {
                result.Errors = GetAccessDeniedErrors();
            }

            return result;
        }
    }
}