﻿using HelpingTools.CalculationTools;
using HelpingTools.ExtentionTools;
using HelpingTools.Interfaces;
using Ninject.Modules;

namespace Dependencies.NinjectModules
{
    public class ToolsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPasswordHashManager>().To<PasswordHashManager>();
            Bind<IExtendedRandom>().To<ExtendedRandom>();
            Bind<IAccountNameCalculator>().To<AccountNameCalculator>();
        }
    }
}