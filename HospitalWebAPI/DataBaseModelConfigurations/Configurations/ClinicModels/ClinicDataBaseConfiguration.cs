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

            this.ToTable("Clinics");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();
            this.Property(model => model.Address).IsRequired();
            this.Property(model => model.IsBlocked).IsRequired();

            // Links to tables

            this.HasMany(model => model.ClinicUsers).WithRequired(link => link.Clinic);
            this.HasMany(model => model.Reservations).WithRequired(link => link.Clinic).HasForeignKey(model => model.ClinicId);
        }
    }
}
