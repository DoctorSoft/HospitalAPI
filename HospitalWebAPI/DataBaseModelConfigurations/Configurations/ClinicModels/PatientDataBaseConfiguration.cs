using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    public class PatientDataBaseConfiguration : EntityTypeConfiguration<PatientStorageModel>
    {
        public PatientDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Patients");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(model => model.Code).IsRequired();

            // Links to tables

            this.HasRequired(model => model.Reservation).WithOptional(link => link.Patient);
        }
    }
}
