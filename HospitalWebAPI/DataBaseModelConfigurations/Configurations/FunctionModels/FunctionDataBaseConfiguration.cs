using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.FunctionModels;

namespace DataBaseModelConfigurations.Configurations.FunctionModels
{
    public class FunctionDataBaseConfiguration : EntityTypeConfiguration<FunctionStorageModel>
    {
        public FunctionDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Functions");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();

            // Links to tables

            this.HasMany(model => model.GroupFunctions).WithRequired(link => link.Function).HasForeignKey(model => model.FunctionId);
            this.HasMany(model => model.UserFunctions).WithRequired(link => link.Function).HasForeignKey(model => model.FunctionId);
        }
    }
}
