using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
using StorageModels.Models.MailboxModels;

namespace Repositories.DataBaseRepositories.MailboxRepositories
{
    public class MessageRepository : AbstractUpdateAbleDataBaseRepository<MessageStorageModel>, IMessageRepository
    {
        public MessageRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
