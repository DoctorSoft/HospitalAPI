using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.HospitalModels;

namespace DataBaseModelConfigurations.Configurations.HospitalModels
{
    public class HospitalDataBaseConfiguration: EntityTypeConfiguration<HospitalStorageModel>
    {
        public HospitalDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Hospitals");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();
            this.Property(model => model.Address).IsRequired();
            this.Property(model => model.IsBlocked).IsRequired();
            this.Property(model => model.IsForChildren).IsRequired();

            // Links to tables

            this.HasMany(model => model.HospitalSectionProfiles).WithRequired(link => link.Hospital).HasForeignKey(model => model.HospitalId);
            this.HasMany(model => model.HospitalUsers).WithRequired(link => link.Hospital).HasForeignKey(model => model.HospitalId);
        }
    }
}
