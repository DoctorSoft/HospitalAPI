using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using StorageModels.Models.FunctionModels;

namespace Repositories.DataBaseRepositories.FunctionRepositories
{
    public class DistributiveGroupRepository : AbstractUpdateAbleDataBaseRepository<DistributiveGroupStorageModel>, IDistributiveGroupRepository
    {
        public DistributiveGroupRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
