using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class AccountDataBaseConfiguration : EntityTypeConfiguration<AccountStorageModel>
    {
        public AccountDataBaseConfiguration()
        {
            
        }
    }
}
