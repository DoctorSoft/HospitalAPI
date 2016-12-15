using DataBaseModelConfigurations.Contexts;
using DataBaseTools.Interfaces;
using Ninject.Modules;
using Repositories.AnotherRepositories.RemoteAPIRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;

namespace Dependencies.NinjectModules
{
    public class RepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Repositories

            //Bind<IDataBaseContext>().To<TestDataBaseContext>().InThreadScope();

            Bind<IPersonDataAPIRepository>().To<PersonDataAPIRepository>().InThreadScope();
        }
    }
}
