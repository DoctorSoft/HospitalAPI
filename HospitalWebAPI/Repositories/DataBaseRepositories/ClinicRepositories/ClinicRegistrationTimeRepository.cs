using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class ClinicRegistrationTimeRepository : AbstractAddAbleDataBaseRepository<ClinicRegistrationTimeStorageModel>, IClinicRegistrationTimeRepository
    {
        public ClinicRegistrationTimeRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
