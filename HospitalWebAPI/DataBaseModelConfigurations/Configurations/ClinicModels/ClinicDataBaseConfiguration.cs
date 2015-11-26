using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels 
{
    public class ClinicDataBaseConfiguration : EntityTypeConfiguration<ClinicStorageModel>
    {
        public ClinicDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Clinic");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();
            this.Property(model => model.Address).IsRequired();

            // Links to tables

            this.HasMany(model => model.ClinicUsers).WithRequired(link => link.Clinic);
            this.HasMany(model => model.Reservations).WithRequired(link => link.Clinic);
        }
    }
}
