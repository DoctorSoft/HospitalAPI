using Dependencies.NinjectModules;
using Ninject;

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
