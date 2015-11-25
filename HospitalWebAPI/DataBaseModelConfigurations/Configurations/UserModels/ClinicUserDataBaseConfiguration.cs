using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class ClinicUserDataBaseConfiguration: EntityTypeConfiguration<ClinicUserStorageModel>
    {
        public ClinicUserDataBaseConfiguration()
        {
            // Table name

            this.ToTable("ClinicUsers");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Links to tables

            this.HasRequired(model => model.User).WithOptional(link => link.ClinicUser);
            this.HasRequired(model => model.Clinic).WithMany(link => link.ClinicUsers);
        }
    }
}
