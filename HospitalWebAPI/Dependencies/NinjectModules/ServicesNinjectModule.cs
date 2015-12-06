using Ninject.Modules;
using Ninject.Web.Common;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
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
            Bind<IAuthorizationService>().To<AuthorizationService>().InRequestScope();
            Bind<ISessionService>().To<SessionService>().InRequestScope();
        }
    }
}
