using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.FunctionModels;

namespace CreateRandomDataTools.DataCreators
{
    public class UserFunctionModelCreator : IUserFunctionModelCreator
    {
        private readonly IUserRepository _userRepository;
        private readonly IFunctionalGroupRepository _functionalGroupRepository;

        public UserFunctionModelCreator(IUserRepository userRepository, IFunctionalGroupRepository functionalGroupRepository)
        {
            _userRepository = userRepository;
            _functionalGroupRepository = functionalGroupRepository;
        }

        private class TypeFunctionSelectorModel
        {
            public int UserTypeId { get; set; }

            public IEnumerable<GroupFunctionStorageModel> GroupFunctions { get; set; }
        }

        public IEnumerable<UserFunctionStorageModel> GetList()
        {
            var functionSelectors = ((IDbSet<FunctionalGroupStorageModel>)_functionalGroupRepository.GetModels())
                .Include(model => model.GroupFunctions)
                .Select(model => new TypeFunctionSelectorModel
                {
                    UserTypeId = model.UserTypeId,
                    GroupFunctions = model.GroupFunctions
                }).ToList();

            var results = _userRepository
                .GetModels()
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
