using Dependencies.NinjectModules;
using Ninject;

namespace Dependencies.NinjectKernels
{
    public class DataBaseFillerKernel : StandardKernel
    {
        public DataBaseFillerKernel()
            : base(new CreatorsNinjectModule(), new RemoteServicesNinjectModule(), new RepositoriesNinjectModule(), new ToolsNinjectModule())
        {
            
        }
    }
}
