using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class ReservationFileRepository : AbstractAddAbleDataBaseRepository<ReservationFileStorageModel>,
        IReservationFileRepository
    {
        public ReservationFileRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
