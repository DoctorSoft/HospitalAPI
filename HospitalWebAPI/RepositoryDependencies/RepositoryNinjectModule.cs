

using DataBaseModelConfigurations.ConfigurationFactories;
using DataBaseModelConfigurations.Contexts;
using DataBaseTools.Interfaces;
using Ninject.Modules;
using Repositories.DataBaseRepositories.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;

namespace RepositoryDependencies
{
    public class RepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataBaseConfigurationFactory>().To<OriginalConfigurationFactory>();
            Bind<IDataBaseContext>().To<TestDataBaseContext>(); // Change it on OriginalDataBaseContext

            // Repositories

            Bind<IClinicRepository>().To<ClinicRepository>();
            Bind<IPatientRepository>().To<PatientRepository>();
            Bind<IReservationRepository>().To<ReservationRepository>();
        }
    }
}
