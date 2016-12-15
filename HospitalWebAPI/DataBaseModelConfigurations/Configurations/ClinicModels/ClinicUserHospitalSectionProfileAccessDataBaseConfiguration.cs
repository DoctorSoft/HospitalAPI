using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    public class ClinicUserHospitalSectionProfileAccessDataBaseConfiguration : EntityTypeConfiguration<ClinicUserHospitalSectionProfileAccessStorageModel>
    {
        public ClinicUserHospitalSectionProfileAccessDataBaseConfiguration()
        {
            // Table name

            this.ToTable("ClinicUserHospitalSectionProfileAccesses");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.IsBlocked).IsRequired();

            // Links to tables

            this.HasRequired(model => model.HospitalSectionProfile).WithMany(link => link.ClinicUserHospitalSectionProfileAccesses).HasForeignKey(model => model.HospitalSectionProfileId).WillCascadeOnDelete(false);
            this.HasRequired(model => model.ClinicUser).WithMany(link => link.ClinicUserHospitalSectionProfileAccessses).HasForeignKey(model => model.ClinicUserId).WillCascadeOnDelete(false);
        }
    }
}
