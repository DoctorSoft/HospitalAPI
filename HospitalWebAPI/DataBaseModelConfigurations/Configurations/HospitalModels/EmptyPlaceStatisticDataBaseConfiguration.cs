using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.MailboxModels;

namespace DataBaseModelConfigurations.Configurations.HospitalModels
{
    public class EmptyPlaceStatisticDataBaseConfiguration : EntityTypeConfiguration<EmptyPlaceStatisticStorageModel>
    {
        public EmptyPlaceStatisticDataBaseConfiguration()
        {
            
        }
    }
}
