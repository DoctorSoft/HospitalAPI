using System;
using System.Linq;
using DataBaseModelConfigurations.ConfigurationFactories;
using DataBaseModelConfigurations.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageModels.Models.UserModels;

namespace Tests.DataBaseTests
{
    [TestClass]
    public class ConnectionTest
    {
        [TestMethod]
        public void DoesConnectionExist()
        {
            var connection = new TestDataBaseContext(new OriginalConfigurationFactory());
            var list = connection.Set<AccountStorageModel>().ToList();
        }
    }
}
