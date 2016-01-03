using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class ClinicHospitalPriorityRepository : AbstractAddAbleDataBaseRepository<ClinicHospitalPriorityStorageModel>, IClinicHospitalPriorityRepository
    {
        public ClinicHospitalPriorityRepository(IDataBaseContext context)
            : base(context)
        {
        }
    }
}
