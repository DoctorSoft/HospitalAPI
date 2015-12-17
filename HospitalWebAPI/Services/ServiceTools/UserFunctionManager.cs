using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.RepositoryHandlers;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using ServiceModels.ModelTools.Entities;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class UserFunctionManager : IUserFunctionManager
    {
        private readonly IUserFunctionRepository _userFunctionRepository;
        private readonly IFunctionRepository _functionRepository;

        private readonly IBlockAbleHandler _blockAbleHandler;
        private readonly ITokenManager _tokenManager;

        public UserFunctionManager(IUserFunctionRepository userFunctionRepository, IFunctionRepository functionRepository, IBlockAbleHandler blockAbleHandler, ITokenManager tokenManager)
        {
            _userFunctionRepository = userFunctionRepository;
            _functionRepository = functionRepository;
            _blockAbleHandler = blockAbleHandler;
            _tokenManager = tokenManager;
        }

        protected virtual IEnumerable<UserFunctionStorageModel> GetUserFunctionsByUser(UserStorageModel user)
        {
            var result = _blockAbleHandler.GetAccessAbleModels(_userFunctionRepository.GetModels())
                .Where(model => model.UserId == user.Id)
                .ToList();

            return result;
        }

        protected virtual IEnumerable<FunctionStorageModel> GetFunctionsByUserFunctions(
            IEnumerable<UserFunctionStorageModel> models)
        {
            var functionIds = models.Select(model => model.FunctionId).ToList();
            var result =
                _blockAbleHandler.GetAccessAbleModels(_functionRepository.GetModels())
                    .Where(model => functionIds.Contains(model.Id))
                    .ToList();

            return result;
        }

        protected virtual bool IsFunctionsEnabled(FunctionIdentityName function)
        {
            if (function == FunctionIdentityName.HospitalUserFillEmptyPlaces)
            {
                return false;
            }

            if (function == FunctionIdentityName.ClinicUserBreakRegistrations)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<FunctionAccess> GetFunctionsByToken(Guid? token)
        {
            var user = _tokenManager.GetUserByToken(token);

            if (user == null)
            {
                return new List<FunctionAccess>();
            }

            var userFunctions = GetUserFunctionsByUser(user);
            var resultFunctions = GetFunctionsByUserFunctions(userFunctions);

            var functionAccesses = resultFunctions.Select(model => new FunctionAccess
            {
                AccessType = IsFunctionsEnabled(model.FunctionIdentityName) ? AccessType.Accepted : AccessType.Disabled,
                FunctionStorageModel = model

            });

            return functionAccesses.ToList();
        }
    }
}
