using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    public class ReservationDataBaseConfiguration : EntityTypeConfiguration<ReservationStorageModel>
    {
        public ReservationDataBaseConfiguration()
        {
            
        }
    }
}
