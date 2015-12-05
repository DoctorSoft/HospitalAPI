using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using StorageModels.Models.FunctionModels;

namespace Repositories.DataBaseRepositories.FunctionRepositories
{
    public class FunctionalGroupRepository : AbstractAddAbleDataBaseRepository<FunctionalGroupStorageModel>, IFunctionalGroupRepository
    {
        public FunctionalGroupRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
