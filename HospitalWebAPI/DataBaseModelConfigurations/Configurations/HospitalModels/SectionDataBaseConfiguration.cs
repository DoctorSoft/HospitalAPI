using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.HospitalModels;

namespace DataBaseModelConfigurations.Configurations.HospitalModels
{
    public class SectionDataBaseConfiguration : EntityTypeConfiguration<SectionStorageModel>
    {
        public SectionDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Sections");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();
            this.Property(model => model.IsBlocked).IsRequired();

            // Links to tables

            this.HasMany(model => model.SectionProfiles).WithRequired(link => link.Section);
        }
    }
}
