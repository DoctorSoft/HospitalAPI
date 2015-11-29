using Ninject.Modules;
using RemoteServicesTools.Interfaces;
using RemoteServicesTools.Tools;

namespace RemoteServicesTools.Dependencies
{
    public class RemoteServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAPIDataBrowser>().To<APIDataBrowser>();
        }
    }
}
