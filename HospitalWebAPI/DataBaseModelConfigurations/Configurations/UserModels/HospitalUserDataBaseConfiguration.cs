using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class HospitalUserDataBaseConfiguration : EntityTypeConfiguration<HospitalUserStorageModel>
    {
        public HospitalUserDataBaseConfiguration()
        {
            // Table name

            this.ToTable("HospitalUsers");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Links to tables

            this.HasRequired(model => model.User).WithOptional(link => link.HospitalUser);
            this.HasRequired(model => model.Hospital).WithMany(link => link.HospitalUsers);
        }
    }
}
