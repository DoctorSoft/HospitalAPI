using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace Repositories.DataBaseRepositories.UserRepositories
{
    public class ClinicUserRepository : AbstractUpdateAbleDataBaseRepository<ClinicUserStorageModel>, IClinicUserRepository
    {
        public ClinicUserRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
