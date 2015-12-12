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
using Services.Interfaces.ServiceTools;
using Services.Interfaces.SessionServices;
using Services.MainMenuServices;
using Services.MainPageServices;
using Services.NoticesService;
using Services.ServiceTools;
using Services.SessionServices;

namespace Dependencies.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Tools
            Bind<ITokenManager>().To<TokenManager>().InThreadScope();

            // Services
            Bind<IAuthorizationService>().To<AuthorizationService>().InThreadScope();
            Bind<ISessionService>().To<SessionService>().InThreadScope();
            Bind<IMainPageService>().To<MainPageService>().InThreadScope();
            Bind<IMainMenuService>().To<MainMenuService>().InThreadScope();
            Bind<IClinicRegistrationsService>().To<ClinicRegistrationsService>().InThreadScope();
            Bind<IHospitalRegistrationsService>().To<HospitalRegistrationsService>().InThreadScope();
            Bind<INoticesService>().To<NoticesService>().InThreadScope();
        }
    }
}
