using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace Repositories.DataBaseRepositories.UserRepositories
{
    public class HospitalUserRepository : AbstractUpdateAbleDataBaseRepository<HospitalUserStorageModel>, IHospitalUserRepository
    {
        public HospitalUserRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
