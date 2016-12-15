using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseTools.Interfaces;
using Enums.Enums;
using ServiceModels.ModelTools.Entities;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class UserFunctionManager : IUserFunctionManager
    {
        private readonly ITokenManager _tokenManager;

        private readonly Dictionary<FunctionIdentityName, Func<Guid, bool>> _functions;

        private readonly IDataBaseContext _context;

        public UserFunctionManager(ITokenManager tokenManager, IDataBaseContext context)
        {
            _tokenManager = tokenManager;
            _context = context;

            _functions = new Dictionary<FunctionIdentityName, Func<Guid, bool>>();  // No items now
        }

        protected virtual IEnumerable<UserFunctionStorageModel> GetUserFunctionsByUser(UserStorageModel user)
        {
            var result = _context.Set<UserFunctionStorageModel>()
                .Where(model => model.UserId == user.Id)
                .Where(model => !model.IsBlocked)
                .ToList();

            return result;
        }

        protected virtual IEnumerable<FunctionStorageModel> GetFunctionsByUserFunctions(
            IEnumerable<UserFunctionStorageModel> models)
        {
            var functionIds = models.Select(model => model.FunctionId).ToList();
            var result =
                _context.Set<FunctionStorageModel>()
                    .Where(model => functionIds.Contains(model.Id))
                    .Where(model => !model.IsBlocked)
                    .ToList();

            return result;
        }

        public virtual bool IsFunctionsEnabled(FunctionIdentityName function, Guid token)
        {
            return !_functions.ContainsKey(function) || _functions[function](token);
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
                AccessType = IsFunctionsEnabled(model.FunctionIdentityName, (Guid)token) ? AccessType.Accepted : AccessType.Disabled,
                FunctionStorageModel = model

            });

            return functionAccesses.ToList();
        }
    }
}
