using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.FunctionModels;

namespace DataBaseModelConfigurations.Configurations.FunctionModels
{
    public class GroupFunctionDataBaseConfiguration : EntityTypeConfiguration<GroupFunctionStorageModel>
    {
        public GroupFunctionDataBaseConfiguration()
        {
            // Table name

            this.ToTable("GroupFunction");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Links to tables

            this.HasRequired(model => model.DistributiveGroup).WithMany(link => link.GroupFunctions);
            this.HasRequired(model => model.Function).WithMany(link => link.GroupFunctions);
        }
    }
}
