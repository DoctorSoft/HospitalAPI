using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    public class ReservationFileDataBaseConfiguration: EntityTypeConfiguration<ReservationFileStorageModel>
    {
        public ReservationFileDataBaseConfiguration()
        {
            // Table name

            this.ToTable("RegistrationFiles");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(model => model.Name);
            Property(model => model.File);

            HasRequired(m => m.Reservation).WithMany(model => model.ReservationFiles).HasForeignKey(model => model.ReservationId);
        }
    }
}
