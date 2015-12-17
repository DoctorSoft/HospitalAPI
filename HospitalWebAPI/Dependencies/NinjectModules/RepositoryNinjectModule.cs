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

namespace Dependencies.NinjectModules
{
    public class RepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Repositories

            Bind<IClinicRepository>().To<ClinicRepository>().InThreadScope();
            Bind<IClinicRegistrationTimeRepository>().To<ClinicRegistrationTimeRepository>().InThreadScope();
            Bind<IPatientRepository>().To<PatientRepository>().InThreadScope();
            Bind<IReservationRepository>().To<ReservationRepository>().InThreadScope();
            Bind<IFunctionalGroupRepository>().To<FunctionalGroupRepository>().InThreadScope();
            Bind<IFunctionRepository>().To<FunctionRepository>().InThreadScope();
            Bind<IGroupFunctionRepository>().To<GroupFunctionRepository>().InThreadScope();
            Bind<IUserFunctionRepository>().To<UserFunctionRepository>().InThreadScope();
            Bind<IEmptyPlaceStatisticRepository>().To<EmptyPlaceStatisticRepository>().InThreadScope();
            Bind<IHospitalRepository>().To<HospitalRepository>().InThreadScope();
            Bind<IHospitalSectionProfileRepository>().To<HospitalSectionProfileRepository>().InThreadScope();
            Bind<ISectionProfileRepository>().To<SectionProfileRepository>().InThreadScope();
            Bind<ISectionRepository>().To<SectionRepository>().InThreadScope();
            Bind<IMessageRepository>().To<MessageRepository>().InThreadScope();
            Bind<IAccountRepository>().To<AccountRepository>().InThreadScope();
            Bind<IClinicUserRepository>().To<ClinicUserRepository>().InThreadScope();
            Bind<IHospitalUserRepository>().To<HospitalUserRepository>().InThreadScope();
            Bind<ISessionRepository>().To<SessionRepository>().InThreadScope();
            Bind<IUserRepository>().To<UserRepository>().InThreadScope();
            Bind<IUserTypeRepository>().To<UserTypeRepository>().InThreadScope();
            Bind<IClinicHospitalAccessRepository>().To<ClinicHospitalAccessRepository>().InThreadScope();

            Bind<IPersonDataAPIRepository>().To<PersonDataAPIRepository>().InThreadScope();
        }
    }
}
