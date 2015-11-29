using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.FunctionModels;

namespace DataBaseModelConfigurations.Configurations.FunctionModels
{
    public class FunctionalGroupDataBaseConfiguration : EntityTypeConfiguration<FunctionalGroupStorageModel>
    {
        public FunctionalGroupDataBaseConfiguration()
        {
            // Table name

            this.ToTable("FunctionalGroups");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();

            // Links to tables

            this.HasMany(model => model.GroupFunctions).WithRequired(link => link.FunctionalGroup).HasForeignKey(model => model.FunctionalGroupId);
            this.HasRequired(model => model.UserType).WithMany(model => model.FunctionalGroups).HasForeignKey(model => model.UserTypeId);
        }
    }
}
