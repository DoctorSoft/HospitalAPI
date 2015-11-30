using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class UserTypeDataBaseConfiguration : EntityTypeConfiguration<UserTypeStorageModel>
    {
        public UserTypeDataBaseConfiguration()
        {
            // Table name

            this.ToTable("UserTypes");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(model => model.IsBlocked).IsRequired();
            this.Property(model => model.UserType).IsRequired();

            // Links to tables

            this.HasMany(model => model.Users).WithRequired(model => model.UserType).HasForeignKey(model => model.UserTypeId);
            this.HasMany(model => model.FunctionalGroups).WithRequired(model => model.UserType).HasForeignKey(model => model.UserTypeId);
        }
    }
}
