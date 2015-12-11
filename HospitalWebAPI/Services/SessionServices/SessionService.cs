using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HandleToolsInterfaces.RepositoryHandlers;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.SessionCommandAnswers;
using ServiceModels.ServiceCommands.SessionCommands;
using Services.Interfaces.SessionServices;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.UserModels;

namespace Services.SessionServices
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserFunctionRepository _userFunctionRepository;
        private readonly IFunctionRepository _functionRepository;
        private readonly IAccountRepository _accountRepository;

        private readonly IBlockAbleHandler _blockAbleHandler;

        public SessionService(ISessionRepository sessionRepository, IUserFunctionRepository userFunctionRepository,
            IFunctionRepository functionRepository, IBlockAbleHandler blockAbleHandler, IAccountRepository accountRepository)
        {
            _sessionRepository = sessionRepository;
            _userFunctionRepository = userFunctionRepository;
            _functionRepository = functionRepository;
            _blockAbleHandler = blockAbleHandler;
            _accountRepository = accountRepository;
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

        protected virtual SessionStorageModel GetSession(IsTokenHasAccessToFunctionCommand command)
        {
            var unblockedSessions = _blockAbleHandler.GetAccessAbleModels(_sessionRepository.GetModels());
            var currentSession = unblockedSessions.FirstOrDefault(model => model.Token == command.Token);

            return currentSession;
        }

        protected virtual UserStorageModel GetUserBySession(SessionStorageModel session)
        {
            var user = _blockAbleHandler.GetAccessAbleModels(((IDbSet<AccountStorageModel>) _accountRepository.GetModels())
                .Include(model => model.User))
                .FirstOrDefault(model => model.Id == session.AccountId)
                .User;

            return user;
        }

        protected virtual IEnumerable<UserFunctionStorageModel> GetUserFunctionsByUser(UserStorageModel user)
        {
            var result =
                _blockAbleHandler.GetAccessAbleModels(_userFunctionRepository.GetModels())
                    .Where(model => model.UserId == user.Id)
                    .ToList();

            return result;
        }

        protected virtual IEnumerable<FunctionStorageModel> GetFunctionsByCommand(IsTokenHasAccessToFunctionCommand command)
        {
            var result =
                _blockAbleHandler.GetAccessAbleModels(_functionRepository.GetModels())
                    .Where(model => command.Functions.Contains(model.FunctionIdentityName))
                    .ToList();

            return result;
        }

        protected virtual bool PossessAllFunctions(IEnumerable<FunctionStorageModel> functions, IEnumerable<UserFunctionStorageModel> userFunctions)
        {
            var result =
                functions.All(model => userFunctions.Select(storageModel => storageModel.FunctionId)
                    .Contains(model.Id));

            return result;
        }

        protected virtual bool CheckPresenceOfToken(IsTokenHasAccessToFunctionCommand command)
        {
            var currentSession = GetSession(command);

            if (currentSession == null)
            {
                return false; 
            }

            var user = GetUserBySession(currentSession);

            if (user == null)
            {
                return false;
            }

            var userFunctions = GetUserFunctionsByUser(user);
            var functions = GetFunctionsByCommand(command);

            var result = PossessAllFunctions(functions, userFunctions);

            return result;
        }

        public IsTokenHasAccessToFunctionCommandAnswer IsTokenHasAccessToFunction(IsTokenHasAccessToFunctionCommand command)
        {
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