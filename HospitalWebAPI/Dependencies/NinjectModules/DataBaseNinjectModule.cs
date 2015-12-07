using DataBaseModelConfigurations.ConfigurationFactories;
using DataBaseModelConfigurations.Contexts;
using DataBaseTools.Interfaces;
using Ninject.Modules;

namespace Dependencies.NinjectModules
{
    public class DataBaseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataBaseConfigurationFactory>().To<OriginalConfigurationFactory>().InThreadScope();
            Bind<IDataBaseContext>().To<TestDataBaseContext>().InThreadScope(); // Change it on OriginalDataBaseContext
        }
    }
}
