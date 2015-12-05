using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.HospitalModels;

namespace Repositories.DataBaseRepositories.HospitalRepositories
{
    public class SectionRepository : AbstractAddAbleDataBaseRepository<SectionStorageModel>, ISectionRepository
    {
        public SectionRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
