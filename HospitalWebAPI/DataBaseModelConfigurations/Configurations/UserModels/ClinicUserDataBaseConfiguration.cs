using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class ClinicUserDataBaseConfiguration: EntityTypeConfiguration<ClinicUserStorageModel>
    {
        public ClinicUserDataBaseConfiguration()
        {
            
        }
    }
}
