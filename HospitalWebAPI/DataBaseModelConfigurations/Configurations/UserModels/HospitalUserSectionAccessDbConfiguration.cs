using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class HospitalUserSectionAccessDbConfiguration : EntityTypeConfiguration<HospitalUserSectionAccessStorageModel>
    {
        public HospitalUserSectionAccessDbConfiguration()
        {
            // Table name

            this.ToTable("HospitalUserSectionAccesses");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.IsBlocked).IsRequired();

            // Links to tables

            this.HasRequired(model => model.HospitalSectionProfile).WithMany(model => model.HospitalUserSectionAccesses).HasForeignKey(model => model.HospitalSectionProfileId).WillCascadeOnDelete(false);
            this.HasRequired(model => model.HospitalUser).WithMany(model => model.HospitalUserSectionAccesses).HasForeignKey(model => model.HospitalUserId).WillCascadeOnDelete(false);
        }
    }
}
