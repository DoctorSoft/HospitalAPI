using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
