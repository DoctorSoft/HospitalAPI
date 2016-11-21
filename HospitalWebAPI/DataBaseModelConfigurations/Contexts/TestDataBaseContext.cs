using DataBaseModelConfigurations.ConfigurationFactories;
using DataBaseTools.AbstractClasses;
using DataBaseTools.Interfaces;

namespace DataBaseModelConfigurations.Contexts
{
    // Warning: Don't commit this file

    public class TestDataBaseContext: AbstractConfiguredContext, IDataBaseContext
    {
        private const string Test_Remote_Db = "Test_Remote_Db"; // "Demo_Remote_Db"; //"Test_Remote_Db";

        public TestDataBaseContext()
            : base(Test_Remote_Db, new OriginalConfigurationFactory())
        {
        }
    }
}
