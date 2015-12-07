using Ninject.Modules;
using Services.AuthorizationServices;
using Services.Interfaces.AuthorizationServices;
using Services.Interfaces.SessionServices;
using Services.SessionServices;

namespace Dependencies.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthorizationService>().To<AuthorizationService>().InThreadScope();
            Bind<ISessionService>().To<SessionService>().InThreadScope();
        }
    }
}
