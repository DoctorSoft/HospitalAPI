using System.Data.Entity;
using DataBaseModelConfigurations.Configurations.ClinicModels;
using DataBaseModelConfigurations.Configurations.FunctionModels;
using DataBaseModelConfigurations.Configurations.HospitalModels;
using DataBaseModelConfigurations.Configurations.MailboxModels;
using DataBaseModelConfigurations.Configurations.UserModels;
using DataBaseTools.Interfaces;
using StorageModels.Models.FunctionModels;

namespace DataBaseModelConfigurations.ConfigurationFactories
{
    public class OriginalConfigurationFactory : IDataBaseConfigurationFactory
    {
        public DbModelBuilder GetConfigurations()
        {
            var builder = new DbModelBuilder();

            // Add some configurations
            builder.Configurations.Add(new AccountDataBaseConfiguration());
            builder.Configurations.Add(new UserDataBaseConfiguration());
            builder.Configurations.Add(new ClinicUserDataBaseConfiguration());
            builder.Configurations.Add(new HospitalUserDataBaseConfiguration());
            builder.Configurations.Add(new SessionDataBaseConfiguration());
            builder.Configurations.Add(new EmptyPlaceStatisticDataBaseConfiguration());
            builder.Configurations.Add(new HospitalDataBaseConfiguration());
            builder.Configurations.Add(new HospitalSectionProfileDataBaseConfiguration());
            builder.Configurations.Add(new SectionDataBaseConfiguration());
            builder.Configurations.Add(new SectionProfileDataBaseConfiguration());
            builder.Configurations.Add(new MessageDataBaseConfiguration());
            builder.Configurations.Add(new ClinicDataBaseConfiguration());
            builder.Configurations.Add(new PatientDataBaseConfiguration());
            builder.Configurations.Add(new ReservationDataBaseConfiguration());
            builder.Configurations.Add(new DistributiveGroupStorageModel());
            builder.Configurations.Add(new FunctionDataBaseConfiguration());
            builder.Configurations.Add(new GroupFunctionDataBaseConfiguration());
            builder.Configurations.Add(new UserFunctionDataBaseConfiguration());

            return builder;
        }
    }
}
