using DataBaseFiller.Interfaces;
using DataBaseModelConfigurations.Dependencies;
using HelpingTools.Dependencies;
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
                                            new RemoteServicesNinjectModule(),
                                            new ToolsNinjectModule());

            return kernel.Get<IDataBaseInfoFiller>();
        }
    }
}
