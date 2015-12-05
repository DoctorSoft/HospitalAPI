using DataBaseModelConfigurations.ConfigurationFactories;
using DataBaseModelConfigurations.Contexts;
using DataBaseTools.Interfaces;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Dependencies.NinjectModules
{
    public class DataBaseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataBaseConfigurationFactory>().To<OriginalConfigurationFactory>().InRequestScope();
            Bind<IDataBaseContext>().To<TestDataBaseContext>().InRequestScope(); // Change it on OriginalDataBaseContext
        }
    }
}
