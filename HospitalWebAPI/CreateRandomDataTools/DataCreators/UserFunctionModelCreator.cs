using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class UserFunctionModelCreator : IUserFunctionModelCreator
    {
        private readonly IDataBaseContext _context;

        public UserFunctionModelCreator(IDataBaseContext context)
        {
            _context = context;
        }

        private class TypeFunctionSelectorModel
        {
            public int UserTypeId { get; set; }

            public IEnumerable<GroupFunctionStorageModel> GroupFunctions { get; set; }
        }

        public IEnumerable<UserFunctionStorageModel> GetList()
        {
            var functionSelectors = _context.Set<FunctionalGroupStorageModel>()
                .Include(model => model.GroupFunctions)
                .Select(model => new TypeFunctionSelectorModel
                {
                    UserTypeId = model.UserTypeId,
                    GroupFunctions = model.GroupFunctions
                }).ToList();

            var results = _context.Set<UserStorageModel>()
                .ToList()
                .SelectMany(model => functionSelectors
                    .Where(selectorModel => selectorModel.UserTypeId == model.UserTypeId)
                    .SelectMany(selectorModel => selectorModel.GroupFunctions)
                    .Select(selectorModel => new UserFunctionStorageModel
                    {
                        FunctionId = selectorModel.FunctionId,
                        IsBlocked = false,
                        UserId = model.Id
                    }));

            return results;
        }
    }
}
