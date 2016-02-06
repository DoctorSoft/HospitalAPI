using Dependencies.NinjectFactories;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Services.AuthorizationServices;
using Services.ClinicRegistrationsServices;
using Services.HospitalRegistrationsService;
using Services.Interfaces.AuthorizationServices;
using Services.Interfaces.ClinicRegistrationsServices;
using Services.Interfaces.HospitalRegistrationsService;
using Services.Interfaces.MainMenuServices;
using Services.Interfaces.MainPageServices;
using Services.Interfaces.NoticesService;
using Services.Interfaces.ReceptionMarkingServices;
using Services.Interfaces.ServiceTools;
using Services.Interfaces.SessionServices;
using Services.Interfaces.StatisticServices;
using Services.MainMenuServices;
using Services.MainPageServices;
using Services.NoticesService;
using Services.ReceptionMarkingServices;
using Services.ServiceTools;
using Services.SessionServices;
using Services.StatisticServices;

namespace Dependencies.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Tools
            Bind<ITokenManager>().To<TokenManager>().InThreadScope();
            Bind<ISettingsManager>().To<SettingsManager>().InThreadScope();
            Bind<IUserFunctionManager>().To<UserFunctionManager>().InThreadScope();
            Bind<IClinicManager>().To<ClinicManager>().InThreadScope();
            Bind<IClinicReservationsManager>().To<ClinicReservationsManager>().InThreadScope();
            Bind<IHospitalManager>().To<HospitalManager>().InThreadScope();

            // Services
            Bind<IAuthorizationService>().To<AuthorizationService>().InThreadScope();
            Bind<ISessionService>().To<SessionService>().InThreadScope();
            Bind<IMainPageService>().To<MainPageService>().InThreadScope();
            Bind<IMainMenuService>().To<MainMenuService>().InThreadScope();
            Bind<IClinicRegistrationsService>().To<ClinicRegistrationsService>().InThreadScope();
            Bind<IHospitalRegistrationsService>().To<HospitalRegistrationsService>().InThreadScope();
            Bind<INoticesService>().To<NoticesService>().InThreadScope();
            Bind<IReceptionMarkingService>().To<ReceptionMarkingService>().InThreadScope();
            Bind<IStatisticService>().To<StatisticService>().InThreadScope();

            //Factories
            Bind<ISessionServiceFactory>().ToFactory();
            Bind<IMainMenuServiceFactory>().ToFactory();
        }
    }
}
