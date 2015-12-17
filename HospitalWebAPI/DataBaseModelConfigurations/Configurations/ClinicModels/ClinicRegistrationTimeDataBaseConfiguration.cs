using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    class ClinicRegistrationTimeDataBaseConfiguration : EntityTypeConfiguration<ClinicRegistrationTimeStorageModel>
    {
        public ClinicRegistrationTimeDataBaseConfiguration()
        {
            // Table name

            this.ToTable("ClinicsRegistrationTime");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.StartTime).IsRequired();
            this.Property(model => model.EndTime).IsRequired();
            this.Property(model => model.DateCreate).IsRequired();

        }
    }
}
