using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using StorageModels.Models.FunctionModels;

namespace Repositories.DataBaseRepositories.FunctionRepositories
{
    public class UserFunctionRepository : AbstractAddAbleDataBaseRepository<UserFunctionStorageModel>, IUserFunctionRepository
    {
        public UserFunctionRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
