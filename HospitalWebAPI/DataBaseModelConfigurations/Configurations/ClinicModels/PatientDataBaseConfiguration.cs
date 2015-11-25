using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.ClinicModels;

namespace DataBaseModelConfigurations.Configurations.ClinicModels
{
    public class PatientDataBaseConfiguration : EntityTypeConfiguration<PatientStorageModel>
    {
        public PatientDataBaseConfiguration()
        {
            
        }
    }
}
