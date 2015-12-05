using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.HospitalModels;

namespace Repositories.DataBaseRepositories.HospitalRepositories
{
    public class HospitalRepository : AbstractAddAbleDataBaseRepository<HospitalStorageModel>, IHospitalRepository
    {
        public HospitalRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
