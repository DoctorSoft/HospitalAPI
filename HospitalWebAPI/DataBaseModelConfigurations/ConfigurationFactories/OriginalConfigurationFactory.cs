using System.Data.Entity;
using DataBaseModelConfigurations.Configurations.UserModels;
using DataBaseTools.Interfaces;

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

            return builder;
        }
    }
}
