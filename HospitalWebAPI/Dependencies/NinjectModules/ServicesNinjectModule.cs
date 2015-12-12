﻿using Ninject.Modules;
using Services.AuthorizationServices;
using Services.Interfaces.AuthorizationServices;
using Services.Interfaces.MainMenuServices;
using Services.Interfaces.MainPageServices;
using Services.Interfaces.ServiceTools;
using Services.Interfaces.SessionServices;
using Services.MainMenuServices;
using Services.MainPageServices;
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
        }
    }
}
