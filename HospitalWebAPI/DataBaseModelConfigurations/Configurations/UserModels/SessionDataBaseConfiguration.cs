using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class SessionDataBaseConfiguration : EntityTypeConfiguration<SessionStorageModel>
    {
        public SessionDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Sessions");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.StartTime).IsRequired();
            this.Property(model => model.Token).IsRequired();

            // Links to tables

            this.HasRequired(model => model.Account).WithMany(link => link.Sessions);
        }
    }
}
