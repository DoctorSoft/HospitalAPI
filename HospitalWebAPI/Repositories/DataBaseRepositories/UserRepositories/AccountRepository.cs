using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace Repositories.DataBaseRepositories.UserRepositories
{
    public class AccountRepository : AbstractUpdateAbleDataBaseRepository<AccountStorageModel>, IAccountRepository
    {
        public AccountRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
