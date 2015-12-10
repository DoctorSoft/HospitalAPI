using HandleTools.RepositoryHandlers;
using HandleToolsInterfaces.RepositoryHandlers;
using HelpingTools.CalculationTools;
using HelpingTools.ExtentionTools;
using HelpingTools.Interfaces;
using Ninject.Modules;

namespace Dependencies.NinjectModules
{
    public class ToolsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPasswordHashManager>().To<PasswordHashManager>().InThreadScope();
            Bind<IExtendedRandom>().To<ExtendedRandom>().InThreadScope();
            Bind<IAccountNameCalculator>().To<AccountNameCalculator>().InThreadScope();
            Bind<IBlockAbleHandler>().To<BlockAbleHandler>().InThreadScope();
        }
    }
}
