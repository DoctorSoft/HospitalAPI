using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class AccountDataBaseConfiguration : EntityTypeConfiguration<AccountStorageModel>
    {
        public AccountDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Accounts");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.HashedPassword).IsRequired();
            this.Property(model => model.Login).IsRequired();

            // Links to tables

            this.HasMany(model => model.Sessions).WithRequired(link => link.Account);
            this.HasRequired(model => model.User).WithOptional(link => link.Account);
        }
    }
}
