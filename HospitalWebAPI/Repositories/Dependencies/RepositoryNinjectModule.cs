using Ninject.Modules;
using Repositories.AnotherRepositories.RemoteAPIRepositories;
using Repositories.DataBaseRepositories.ClinicRepositories;
using Repositories.DataBaseRepositories.FunctionRepositories;
using Repositories.DataBaseRepositories.HospitalRepositories;
using Repositories.DataBaseRepositories.MailboxRepositories;
using Repositories.DataBaseRepositories.UserRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;

namespace Repositories.Dependencies
{
    public class RepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Repositories

            Bind<IClinicRepository>().To<ClinicRepository>();
            Bind<IPatientRepository>().To<PatientRepository>();
            Bind<IReservationRepository>().To<ReservationRepository>();
            Bind<IFunctionalGroupRepository>().To<FunctionalGroupRepository>();
            Bind<IFunctionRepository>().To<FunctionRepository>();
            Bind<IGroupFunctionRepository>().To<GroupFunctionRepository>();
            Bind<IUserFunctionRepository>().To<UserFunctionRepository>();
            Bind<IEmptyPlaceStatisticRepository>().To<EmptyPlaceStatisticRepository>();
            Bind<IHospitalRepository>().To<HospitalRepository>();
            Bind<IHospitalSectionProfileRepository>().To<HospitalSectionProfileRepository>();
            Bind<ISectionProfileRepository>().To<SectionProfileRepository>();
            Bind<ISectionRepository>().To<SectionRepository>();
            Bind<IMessageRepository>().To<MessageRepository>();
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IClinicUserRepository>().To<ClinicUserRepository>();
            Bind<IHospitalUserRepository>().To<HospitalUserRepository>();
            Bind<ISessionRepository>().To<SessionRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserTypeRepository>().To<UserTypeRepository>();
            Bind<IClinicHospitalAccessRepository>().To<ClinicHospitalAccessRepository>();

            Bind<IPersonDataAPIRepository>().To<PersonDataAPIRepository>();
        }
    }
}
