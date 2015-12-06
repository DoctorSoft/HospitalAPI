using Dependencies.NinjectModules;
using Ninject;
using Ninject.Web.WebApi;

namespace Dependencies.NinjectKernels
{
    public class HospitalWebAPIKernel : StandardKernel
    {
        public HospitalWebAPIKernel()
            : base(new RemoteServicesNinjectModule(), new RepositoriesNinjectModule(), new ToolsNinjectModule(), new ServicesNinjectModule(), new DataBaseNinjectModule())
        {
            
        }
    }
}
