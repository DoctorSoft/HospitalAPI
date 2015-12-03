using System.Collections.Generic;
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
        private readonly IGroupFunctionRepository _groupFunctionRepository;

        public UserFunctionModelCreator(IUserRepository userRepository, IGroupFunctionRepository groupFunctionRepository)
        {
            _userRepository = userRepository;
            _groupFunctionRepository = groupFunctionRepository;
        }

        public IEnumerable<UserFunctionStorageModel> GetList()
        {
            var users = _userRepository.GetModels().ToList();
            var results = users.SelectMany(model => GetFunctionsList(model.UserTypeId));

            return results;
        }

        protected virtual IEnumerable<UserFunctionStorageModel> GetFunctionsList(int userTypeId)
        {
            var groupFunctions = _groupFunctionRepository.GetModels().ToList();
            var result = groupFunctions.Where(model => model.Id == userTypeId);

            return null;
        }
    }
}
