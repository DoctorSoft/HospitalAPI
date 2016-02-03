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
        private const string ArturTomashHomeKey = "TestHospitalDataBase_ArturTomashHome";
        private const string VaniaHomeKey = "TestHospitalDataBase_VaniaHome";
        private const string TomashPrivateKey = "TestHospitalDataBase_TomashPrivate";
        private const string ArturWorkKey = "TestHospitalDataBase_ArturWork";
        private const string VaniaWorkKey = "TestHospitalDataBase_VaniaWork";

        private const string ArturTomashHomeOriginalKey = "OriginalHospitalDataBase_ArturTomashHome";
        private const string VaniaHomeOriginalKey = "OriginalHospitalDataBase_VaniaHome";
        private const string TomashPrivateOriginalKey = "OriginalHospitalDataBase_TomashPrivate";
        private const string ArturWorkOriginalKey = "OriginalHospitalDataBase_ArturWork";
        private const string VaniaWorkOriginalKey = "OriginalHospitalDataBase_VaniaWork";

        private const string Test_Remote_Db = "Test_Remote_Db";

        public TestDataBaseContext()
            : base(ArturTomashHomeKey, new OriginalConfigurationFactory())
        {
        }
    }
}
