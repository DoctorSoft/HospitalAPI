using Dependencies.NinjectModules;
using Ninject;

namespace Dependencies.NinjectKernels
{
    public class HospitalMVCKernel: StandardKernel
    {
        public HospitalMVCKernel()
            : base(new RemoteServicesNinjectModule(), new RepositoriesNinjectModule(), new ToolsNinjectModule(), new ServicesNinjectModule(), new DataBaseNinjectModule())
        {
            
        }
    }
}
