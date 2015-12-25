using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class ClinicHospitalAccessRepository : AbstractAddAbleDataBaseRepository<ClinicHospitalPriorityStorageModel>, IClinicHospitalAccessRepository
    {
        public ClinicHospitalAccessRepository(IDataBaseContext context)
            : base(context)
        {
        }
    }
}
