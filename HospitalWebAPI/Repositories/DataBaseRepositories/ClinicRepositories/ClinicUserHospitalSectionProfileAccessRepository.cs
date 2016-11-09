using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class ClinicUserHospitalSectionProfileAccessRepository : AbstractAddAbleDataBaseRepository<ClinicUserHospitalSectionProfileAccessStorageModel>, IClinicUserHospitalSectionProfileAccessRepository
    {
        public ClinicUserHospitalSectionProfileAccessRepository(IDataBaseContext context)
            : base(context)
        {
        }
    }
}
