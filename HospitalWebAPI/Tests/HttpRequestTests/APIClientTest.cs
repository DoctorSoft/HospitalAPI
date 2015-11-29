using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteServicesTools.Tools;

namespace Tests.HttpRequestTests
{
    [TestClass]
    public class APIClientTest
    {
        [TestMethod]
        public void GetStringData()
        {
            var client = new APIDataBrowser();
            var url = "http://randus.ru/api.php";
            var stringData = client.GetData(url);
        }

        [TestMethod]
        public void GetObjectData()
        {
            var client = new APIDataBrowser();
            var url = "http://randus.ru/api.php";
            var objectData = client.GetData<Dictionary<string, object>>(url);
        }
    }
}
