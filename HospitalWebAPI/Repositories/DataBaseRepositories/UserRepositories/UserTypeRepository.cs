using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace Repositories.DataBaseRepositories.UserRepositories
{
    public class UserTypeRepository: AbstractUpdateAbleDataBaseRepository<UserTypeStorageModel>, IUserTypeRepository
    {
        public UserTypeRepository(IDataBaseContext context)
            : base(context)
        {
        }
    }
}
