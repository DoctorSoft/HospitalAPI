using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class UserDataBaseConfiguration: EntityTypeConfiguration<UserStorageModel>
    {
        public UserDataBaseConfiguration()
        {
            
        }
    }
}
