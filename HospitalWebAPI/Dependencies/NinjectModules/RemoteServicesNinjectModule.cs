using Ninject.Modules;
using Ninject.Web.Common;
using RemoteServicesTools.Interfaces;
using RemoteServicesTools.Tools;

namespace Dependencies.NinjectModules
{
    public class RemoteServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAPIDataBrowser>().To<APIDataBrowser>().InRequestScope();
        }
    }
}
