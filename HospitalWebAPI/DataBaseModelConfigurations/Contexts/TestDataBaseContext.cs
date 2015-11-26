using DataBaseModelConfigurations.ConfigurationFactories;
using DataBaseTools.AbstractClasses;
using DataBaseTools.Interfaces;

namespace DataBaseModelConfigurations.Contexts
{
    public class TestDataBaseContext: AbstractConfiguredContext
    {
        public TestDataBaseContext() : base("TestHospitalDataBase", new OriginalConfigurationFactory())
        {
        }
    }
}
