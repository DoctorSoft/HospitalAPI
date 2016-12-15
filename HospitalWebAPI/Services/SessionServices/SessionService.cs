using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseTools.Interfaces;
using Enums.Enums;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.SessionCommandAnswers;
using ServiceModels.ServiceCommands.SessionCommands;
using Services.Interfaces.ServiceTools;
using Services.Interfaces.SessionServices;
using StorageModels.Models.FunctionModels;

namespace Services.SessionServices
{
    public class SessionService : ISessionService
    {
        private readonly IUserFunctionManager _userFunctionManager;

        private readonly IDataBaseContext _context;

        public SessionService(IUserFunctionManager userFunctionManager, IDataBaseContext context)
        {
            _userFunctionManager = userFunctionManager;
            _context = context;
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

        protected virtual IEnumerable<FunctionStorageModel> GetFunctionsByCommand(IsTokenHasAccessToFunctionCommand command)
        {
            var result =
                _context.Set<FunctionStorageModel>()
                    .Where(model => command.Functions.Contains(model.FunctionIdentityName))
                    .Where(model => !model.IsBlocked)
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

            var result = PossessAllFunctions(functions, userFunctions.Select(access => access.FunctionStorageModel));

            var checkedFunctions = userFunctions
                .Where(item => functions.Select(value => value.FunctionIdentityName).Contains(item.FunctionStorageModel.FunctionIdentityName));

            if (!result)
            {
                return AccessType.Redirected;
            }

            if (checkedFunctions.Any(access => access.AccessType == AccessType.Disabled))
            {
                return AccessType.Disabled;
            }

            return AccessType.Accepted;
        }

        public IsTokenHasAccessToFunctionCommandAnswer IsTokenHasAccessToFunction(IsTokenHasAccessToFunctionCommand command)
        {
            if (command.Token == null)
            {
                return new IsTokenHasAccessToFunctionCommandAnswer
                {
                    Errors = GetAccessDeniedErrors(),
                    AccessType = AccessType.Denied
                };
            }
            
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