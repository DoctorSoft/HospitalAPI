using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using StorageModels.Models.FunctionModels;

namespace Repositories.DataBaseRepositories.FunctionRepositories
{
    public class GroupFunctionRepository : AbstractUpdateAbleDataBaseRepository<GroupFunctionStorageModel>, IGroupFunctionRepository
    {
        public GroupFunctionRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
