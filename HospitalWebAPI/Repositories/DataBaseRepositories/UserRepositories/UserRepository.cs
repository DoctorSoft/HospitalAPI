using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace Repositories.DataBaseRepositories.UserRepositories
{
    public class UserRepository : AbstractUpdateAbleDataBaseRepository<UserStorageModel>, IUserRepository
    {
        public UserRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
