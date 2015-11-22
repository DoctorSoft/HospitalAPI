using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseModelConfigurations.Contexts;
using DataBaseTools.Interfaces;
using Ninject.Modules;

namespace RepositoryDependencies
{
    public class RepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataBaseContext>().To<OriginalDataBaseContext>();

        }
    }
}
