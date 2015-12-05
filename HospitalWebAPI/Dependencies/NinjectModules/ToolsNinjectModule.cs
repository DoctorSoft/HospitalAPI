using HelpingTools.CalculationTools;
using HelpingTools.ExtentionTools;
using HelpingTools.Interfaces;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Dependencies.NinjectModules
{
    public class ToolsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPasswordHashManager>().To<PasswordHashManager>().InRequestScope();
            Bind<IExtendedRandom>().To<ExtendedRandom>().InRequestScope();
            Bind<IAccountNameCalculator>().To<AccountNameCalculator>().InRequestScope();
        }
    }
}
