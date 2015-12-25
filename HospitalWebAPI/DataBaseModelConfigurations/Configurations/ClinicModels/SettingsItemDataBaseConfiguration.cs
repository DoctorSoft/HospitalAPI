using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    public class SettingsItemDataBaseConfiguration : EntityTypeConfiguration<SettingsItemStorageModel>
    {
        public SettingsItemDataBaseConfiguration()
        {
            // Table name

            this.ToTable("SettingsItems");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.DateCreate).IsRequired();

        }
    }
}
