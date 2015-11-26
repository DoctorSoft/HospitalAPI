using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.MailboxModels;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.MailboxModels
{
    public class MessageDataBaseConfiguration : EntityTypeConfiguration<MessageStorageModel>
    {
        public MessageDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Messages");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Title).IsRequired();
            this.Property(model => model.Text).IsRequired();
            this.Property(model => model.MessageType).IsRequired();
            this.Property(model => model.IsRead).IsRequired();
            this.Property(model => model.Date).IsRequired();
            this.Property(model => model.ShowStatus).IsRequired();

            // Links to tables

            this.HasRequired(model => model.UserFrom).WithMany(link => link.MessagesFrom);
            this.HasRequired(model => model.UserTo).WithMany(link => link.MessagesTo);
        }
    }
}
