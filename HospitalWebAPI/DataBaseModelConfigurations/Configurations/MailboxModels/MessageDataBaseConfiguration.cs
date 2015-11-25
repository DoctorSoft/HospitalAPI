using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.MailboxModels;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.MailboxModels
{
    public class MessageDataBaseConfiguration : EntityTypeConfiguration<MessageStorageModel>
    {
        public MessageDataBaseConfiguration()
        {
            
        }
    }
}
