using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.MailboxModels;

namespace DataBaseModelConfigurations.Configurations.MailboxModels
{
    public class DischangeDataBaseConfiguration : EntityTypeConfiguration<DischargeStorageModel>
    {
        public DischangeDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Discharges");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Body).IsRequired();
            this.Property(model => model.MimeType).IsRequired();
            this.Property(model => model.Name).IsRequired();

            // Links to tables

            this.HasRequired(model => model.Message).WithMany(link => link.Discharges).HasForeignKey(model => model.MessageId);
        }
    }
}
