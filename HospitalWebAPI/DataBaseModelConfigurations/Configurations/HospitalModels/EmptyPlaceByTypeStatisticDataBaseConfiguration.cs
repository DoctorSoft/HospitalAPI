using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.HospitalModels;

namespace DataBaseModelConfigurations.Configurations.HospitalModels
{
    public class EmptyPlaceByTypeStatisticDataBaseConfiguration : EntityTypeConfiguration<EmptyPlaceByTypeStatisticStorageModel>
    {
        public EmptyPlaceByTypeStatisticDataBaseConfiguration()
        {
            // Table name

            this.ToTable("EmptyPlaceByTypeStatistics");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Sex).IsOptional();
            this.Property(model => model.Count).IsRequired();

            // Links to tables

            this.HasMany(model => model.Reservations).WithRequired(link => link.EmptyPlaceByTypeStatistic).HasForeignKey(model => model.EmptyPlaceByTypeStatisticId);
            this.HasRequired(model => model.EmptyPlaceStatistic).WithMany(link => link.EmptyPlaceByTypeStatistics).HasForeignKey(model => model.EmptyPlaceStatisticId);  
        }
    }
}
