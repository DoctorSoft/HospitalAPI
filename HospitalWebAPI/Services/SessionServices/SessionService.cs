using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
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
        private readonly IUserFunctionManager _userFunctionManager;

        public SessionService(IUserFunctionRepository userFunctionRepository,
            IFunctionRepository functionRepository, IBlockAbleHandler blockAbleHandler, ITokenManager tokenManager, IUserFunctionManager userFunctionManager)
        {
            _userFunctionRepository = userFunctionRepository;
            _functionRepository = functionRepository;
            _blockAbleHandler = blockAbleHandler;
            _userFunctionManager = userFunctionManager;
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

        protected virtual bool PossessAllFunctions(IEnumerable<FunctionStorageModel> functions, IEnumerable<FunctionStorageModel> userFunctions)
        {
            var result =
                functions.All(model => userFunctions.Select(storageModel => storageModel.Id)
                    .Contains(model.Id));

            return result;
        }

        protected virtual AccessType CheckPresenceOfToken(IsTokenHasAccessToFunctionCommand command)
        {
            var userFunctions = _userFunctionManager.GetFunctionsByToken(command.Token);

            if (!userFunctions.Any())
            {
                return AccessType.Denied;
            }

            var functions = GetFunctionsByCommand(command);

            var result = PossessAllFunctions(functions, userFunctions);

            return result ? AccessType.Accepted : AccessType.Redirected;
        }

        public IsTokenHasAccessToFunctionCommandAnswer IsTokenHasAccessToFunction(IsTokenHasAccessToFunctionCommand command)
        {
            var result = new IsTokenHasAccessToFunctionCommandAnswer
            {
                AccessType = CheckPresenceOfToken(command),
                Token = (Guid)command.Token
            };

            if (result.AccessType == AccessType.Denied)
            {
                result.Errors = GetAccessDeniedErrors();
            }

            return result;
        }
    }
}