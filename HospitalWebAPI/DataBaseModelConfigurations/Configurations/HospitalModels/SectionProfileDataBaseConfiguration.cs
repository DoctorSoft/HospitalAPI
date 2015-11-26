using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.HospitalModels;

namespace DataBaseModelConfigurations.Configurations.HospitalModels
{
    public class SectionProfileDataBaseConfiguration : EntityTypeConfiguration<SectionProfileStorageModel>
    {
        public SectionProfileDataBaseConfiguration()
        {
            // Table name

            this.ToTable("SectionProfile");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();

            // Links to tables

            this.HasRequired(model => model.Section).WithMany(link => link.SectionProfiles);
            this.HasMany(model => model.HospitalSectionProfiles).WithOptional(link => link.SectionProfile);
        }
    }
}
