using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
using StorageModels.Models.MailboxModels;

namespace Repositories.DataBaseRepositories.MailboxRepositories
{
    public class DischargeRepository : AbstractAddAbleDataBaseRepository<DischargeStorageModel>, IDischargeRepository
    {
        public DischargeRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
