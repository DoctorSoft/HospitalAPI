using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.FunctionModels;

namespace DataBaseModelConfigurations.Configurations.FunctionModels
{
    public class UserFunctionDataBaseConfiguration : EntityTypeConfiguration<UserFunctionStorageModel>
    {
        public UserFunctionDataBaseConfiguration()
        {
            // Table name

            this.ToTable("UserFunctions");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Links to tables

            this.HasRequired(model => model.Function).WithMany(link => link.UserFunctions).HasForeignKey(model => model.FunctionId);
            this.HasRequired(model => model.User).WithMany(link => link.UserFunctions).HasForeignKey(model => model.UserId);  
        }
    }
}
