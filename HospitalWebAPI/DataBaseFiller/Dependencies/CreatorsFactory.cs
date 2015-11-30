using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseFiller.Interfaces;
using DataBaseModelConfigurations.Dependencies;
using Ninject;
using RemoteServicesTools.Dependencies;
using Repositories.Dependencies;

namespace DataBaseFiller.Dependencies
{
    public class CreatorsFactory 
    {
        public IDataBaseInfoFiller GetFiller()
        {
            var kernel = new StandardKernel(new DataBaseNinjectModule(),
                                            new CreatorsNinjectModule(), 
                                            new RepositoriesNinjectModule(), 
                                            new RemoteServicesNinjectModule());

            return kernel.Get<IDataBaseInfoFiller>();
        }
    }
}
