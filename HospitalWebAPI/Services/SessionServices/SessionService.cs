using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HandleToolsInterfaces.RepositoryHandlers;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.SessionCommandAnswers;
using ServiceModels.ServiceCommands.SessionCommands;
using Services.Interfaces.ServiceTools;
using Services.Interfaces.SessionServices;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.UserModels;

namespace Services.SessionServices
{
    public class SessionService : ISessionService
    {
        private readonly IUserFunctionRepository _userFunctionRepository;
        private readonly IFunctionRepository _functionRepository;

        private readonly IBlockAbleHandler _blockAbleHandler;
        private readonly ITokenManager _tokenManager;

        public SessionService(IUserFunctionRepository userFunctionRepository,
            IFunctionRepository functionRepository, IBlockAbleHandler blockAbleHandler, ITokenManager tokenManager)
        {
            _userFunctionRepository = userFunctionRepository;
            _functionRepository = functionRepository;
            _blockAbleHandler = blockAbleHandler;
            this._tokenManager = tokenManager;
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

        protected virtual IEnumerable<UserFunctionStorageModel> GetUserFunctionsByUser(UserStorageModel user)
        {
            var result = _blockAbleHandler.GetAccessAbleModels(_userFunctionRepository.GetModels())
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
            var user = _tokenManager.GetUserByToken(command.Token);

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
                HasAccess = CheckPresenceOfToken(command),
                Token = (Guid)command.Token
            };

            if (!result.HasAccess)
            {
                result.Errors = GetAccessDeniedErrors();
            }

            return result;
        }
    }
}