using Dependencies.NinjectModules;
using Ninject;

namespace Dependencies.NinjectKernels
{
    public class TestAppKernel : StandardKernel
    {
        public TestAppKernel()
            : base(new RemoteServicesNinjectModule(), new RepositoriesNinjectModule(), new ToolsNinjectModule(), new ServicesNinjectModule(), new DataBaseNinjectModule())
        {

        }
    }
}
