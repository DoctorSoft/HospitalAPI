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

            this.ToTable("Hospital");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();
            this.Property(model => model.Address).IsRequired();

            // Links to tables

            this.HasMany(model => model.HospitalSectionProfiles).WithRequired(link => link.Hospital);
            this.HasMany(model => model.HospitalUsers).WithRequired(link => link.Hospital);
        }
    }
}
