using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    public class ReservationDataBaseConfiguration : EntityTypeConfiguration<ReservationStorageModel>
    {
        public ReservationDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Reservations");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Status).IsRequired();
            this.Property(model => model.ApproveTime).IsRequired();
            this.Property(model => model.CancelTime).IsOptional();

            // Links to tables

            this.HasRequired(model => model.Clinic).WithMany(link => link.Reservations);
            this.HasOptional(model => model.Patient).WithRequired(link => link.Reservation);
            this.HasRequired(model => model.EmptyPlaceStatistic).WithMany(link => link.Reservations);
        }
    }
}
