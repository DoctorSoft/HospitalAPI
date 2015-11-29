using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.HospitalModels;

namespace DataBaseModelConfigurations.Configurations.HospitalModels
{
    public class HospitalSectionProfileDataBaseConfiguration : EntityTypeConfiguration<HospitalSectionProfileStorageModel>
    {
        public HospitalSectionProfileDataBaseConfiguration()
        {
            // Table name

            this.ToTable("HospitalSectionProfiles");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Date).IsRequired();
            this.Property(model => model.IsBlocked).IsRequired();

            // Links to tables

            this.HasRequired(model => model.Hospital).WithMany(link => link.HospitalSectionProfiles).HasForeignKey(model => model.HospitalId);
            this.HasRequired(model => model.SectionProfile).WithMany(link => link.HospitalSectionProfiles).HasForeignKey(model => model.SectionProfileId);
            this.HasMany(model => model.EmptyPlaceStatistics).WithRequired(link => link.HospitalSectionProfile).HasForeignKey(model => model.HospitalSectionProfileId);
        }
    }
}
