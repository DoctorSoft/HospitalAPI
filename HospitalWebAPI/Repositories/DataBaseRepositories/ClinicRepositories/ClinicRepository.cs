using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class ClinicRepository : AbstractAddAbleDataBaseRepository<ClinicStorageModel>, IClinicRepository
    {
        public ClinicRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
