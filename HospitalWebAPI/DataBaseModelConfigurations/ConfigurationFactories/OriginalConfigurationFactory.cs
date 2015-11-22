using System.Data.Entity;
using DataBaseTools.Interfaces;

namespace DataBaseModelConfigurations.ConfigurationFactories
{
    public class OriginalConfigurationFactory : IDataBaseConfigurationFactory
    {
        public DbModelBuilder GetConfigurations()
        {
            var builder = new DbModelBuilder();

            // Add some configurations

            return builder;
        }
    }
}
