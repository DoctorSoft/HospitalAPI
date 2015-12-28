using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.MailboxModels;

namespace DataBaseModelConfigurations.Configurations.HospitalModels
{
    public class EmptyPlaceStatisticDataBaseConfiguration : EntityTypeConfiguration<EmptyPlaceStatisticStorageModel>
    {
        public EmptyPlaceStatisticDataBaseConfiguration()
        {
            // Table name

            this.ToTable("EmptyPlaceStatistics");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Date).IsRequired();
            this.Property(model => model.CreateTime).IsOptional();

            // Links to tables

            this.HasMany(model => model.EmptyPlaceByTypeStatistics).WithRequired(link => link.EmptyPlaceStatistic).HasForeignKey(model => model.EmptyPlaceStatisticId);
            this.HasRequired(model => model.HospitalSectionProfile).WithMany(link => link.EmptyPlaceStatistics).HasForeignKey(model => model.HospitalSectionProfileId);  
        }
    }
}
