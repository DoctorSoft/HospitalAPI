using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    public class ClinicHospitalPriorityDataBaseConfiguration : EntityTypeConfiguration<ClinicHospitalPriorityStorageModel>
    {
        public ClinicHospitalPriorityDataBaseConfiguration()
        {
            // Table name

            this.ToTable("ClinicHospitalPriorities");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.IsBlocked).IsRequired();
            this.Property(model => model.Priority).IsRequired();

            // Links to tables

            this.HasRequired(model => model.Hospital).WithMany(link => link.ClinicHospitalAccesses).HasForeignKey(model => model.HospitalId);
            this.HasRequired(model => model.Clinic).WithMany(link => link.ClinicHospitalAccesses).HasForeignKey(model => model.ClinicId);
        }
    }
}
