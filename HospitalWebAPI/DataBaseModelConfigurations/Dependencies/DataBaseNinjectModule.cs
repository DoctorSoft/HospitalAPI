using DataBaseModelConfigurations.ConfigurationFactories;
using DataBaseModelConfigurations.Contexts;
using DataBaseTools.Interfaces;
using Ninject.Modules;

namespace DataBaseModelConfigurations.Dependencies
{
    public class DataBaseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataBaseConfigurationFactory>().To<OriginalConfigurationFactory>();
            Bind<IDataBaseContext>().To<TestDataBaseContext>(); // Change it on OriginalDataBaseContext
        }
    }
}
