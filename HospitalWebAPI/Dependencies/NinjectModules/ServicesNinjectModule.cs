using Ninject.Modules;
using Services.AuthorizationServices;
using Services.Interfaces.AuthorizationServices;

namespace Dependencies.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthorizationService>().To<AuthorizationService>();
        }
    }
}
