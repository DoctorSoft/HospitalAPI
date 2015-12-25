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
            this.Property(model => model.Sex).IsRequired();
            this.Property(model => model.Age).IsRequired();
            this.Property(model => model.FirstName).IsRequired();
            this.Property(model => model.LastName).IsRequired();
            this.Property(model => model.PhoneNumber).IsRequired();

            // Links to tables

            this.HasRequired(model => model.Reservation).WithOptional(link => link.Patient);

            //
        }
    }
}
