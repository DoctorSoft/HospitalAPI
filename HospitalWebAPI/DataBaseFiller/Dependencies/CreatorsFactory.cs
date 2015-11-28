using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseFiller.Interfaces;
using DataBaseModelConfigurations.Dependencies;
using Ninject;

namespace DataBaseFiller.Dependencies
{
    public class CreatorsFactory 
    {
        public IDataBaseInfoFiller GetFiller()
        {
            var kernel = new StandardKernel(new DataBaseNinjectModule(), new CreatorsNinjectModule());

            return kernel.Get<IDataBaseInfoFiller>();
        }
    }
}
