using RepositoryTools.Interfaces.CommonInterfaces;
using StorageModels.Models.MailboxModels;

namespace RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories
{
    public interface IMessageRepository : IUpdateAbleRepository<MessageStorageModel>
    {
    }
}
