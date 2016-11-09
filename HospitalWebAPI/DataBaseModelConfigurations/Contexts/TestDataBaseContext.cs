using System.Collections.Generic;
using DataBaseModelConfigurations.ConfigurationFactories;
using DataBaseTools.AbstractClasses;
using DataBaseTools.Interfaces;
using EntityFramework.BulkInsert.Extensions;

namespace DataBaseModelConfigurations.Contexts
{
    // Warning: Don't commit this file

    public class TestDataBaseContext: AbstractConfiguredContext
    {
        private const string Test_Remote_Db = "TestHospitalDataBase_ArturTomashHome"; //"Test_Remote_Db";

        public TestDataBaseContext()
            : base(Test_Remote_Db, new OriginalConfigurationFactory())
        {
        }
    }
}
