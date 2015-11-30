using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using StorageModels.Models.FunctionModels;

namespace Repositories.DataBaseRepositories.FunctionRepositories
{
    public class FunctionalGroupRepository : AbstractUpdateAbleDataBaseRepository<FunctionalGroupStorageModel>, IFunctionalGroupRepository
    {
        public FunctionalGroupRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
