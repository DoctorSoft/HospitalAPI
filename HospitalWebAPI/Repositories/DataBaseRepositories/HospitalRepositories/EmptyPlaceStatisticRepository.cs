using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.HospitalModels;

namespace Repositories.DataBaseRepositories.HospitalRepositories
{
    public class EmptyPlaceStatisticRepository : AbstractAddAbleDataBaseRepository<EmptyPlaceStatisticStorageModel>, IEmptyPlaceStatisticRepository
    {
        public EmptyPlaceStatisticRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
