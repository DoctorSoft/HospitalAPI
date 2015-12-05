using Ninject.Modules;
using Ninject.Web.Common;
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

            Bind<IClinicRepository>().To<ClinicRepository>().InRequestScope();
            Bind<IPatientRepository>().To<PatientRepository>().InRequestScope();
            Bind<IReservationRepository>().To<ReservationRepository>().InRequestScope();
            Bind<IFunctionalGroupRepository>().To<FunctionalGroupRepository>().InRequestScope();
            Bind<IFunctionRepository>().To<FunctionRepository>().InRequestScope();
            Bind<IGroupFunctionRepository>().To<GroupFunctionRepository>().InRequestScope();
            Bind<IUserFunctionRepository>().To<UserFunctionRepository>().InRequestScope();
            Bind<IEmptyPlaceStatisticRepository>().To<EmptyPlaceStatisticRepository>().InRequestScope();
            Bind<IHospitalRepository>().To<HospitalRepository>().InRequestScope();
            Bind<IHospitalSectionProfileRepository>().To<HospitalSectionProfileRepository>().InRequestScope();
            Bind<ISectionProfileRepository>().To<SectionProfileRepository>().InRequestScope();
            Bind<ISectionRepository>().To<SectionRepository>().InRequestScope();
            Bind<IMessageRepository>().To<MessageRepository>().InRequestScope();
            Bind<IAccountRepository>().To<AccountRepository>().InRequestScope();
            Bind<IClinicUserRepository>().To<ClinicUserRepository>().InRequestScope();
            Bind<IHospitalUserRepository>().To<HospitalUserRepository>().InRequestScope();
            Bind<ISessionRepository>().To<SessionRepository>().InRequestScope();
            Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            Bind<IUserTypeRepository>().To<UserTypeRepository>().InRequestScope();
            Bind<IClinicHospitalAccessRepository>().To<ClinicHospitalAccessRepository>().InRequestScope();

            Bind<IPersonDataAPIRepository>().To<PersonDataAPIRepository>().InRequestScope();
        }
    }
}
