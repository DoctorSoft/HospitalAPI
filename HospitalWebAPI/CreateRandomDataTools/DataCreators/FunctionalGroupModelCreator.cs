using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Repositories.DataBaseRepositories.UserRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.FunctionModels;

namespace CreateRandomDataTools.DataCreators
{
    public class FunctionalGroupModelCreator : IFunctionalGroupModelCreator
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private const string GroupNameMark = "Group";

        public FunctionalGroupModelCreator(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        public IEnumerable<FunctionalGroupStorageModel> GetList()
        {
            var userTypes = _userTypeRepository.GetModels().ToList();
            var results = userTypes.Select(model => new FunctionalGroupStorageModel
            {
                Name = string.Format("{0}{1}", model.Name, GroupNameMark),
                UserTypeId = model.Id,

            }).ToList();

            //TODO: Add functions

            return results;
        }
    }
}
