using System.Collections.Generic;
using System.Web.Configuration;
using Dependencies.NinjectKernels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using RemoteServicesTools.Interfaces;

namespace Tests.APIServiceTests
{
    [TestClass]
    public class APIUnitTest
    {
        private string _apiHostingName;
        private IAPIDataBrowser _apiDataBrowser;
        private readonly IKernel _appKernel = new TestAppKernel();
        private const string Login = "Admin";
        private const string Password = "12345";


        [TestInitialize]
        public void Init()
        {
            _apiHostingName = WebConfigurationManager.AppSettings["APIHostingName"] + "api/authorization";
            _apiDataBrowser = _appKernel.Get<IAPIDataBrowser>();
        }

        [TestMethod]
        public void CreateTokenForUser()
        {
            var bodyParameters = new Dictionary<string, string> {{"Login", Login}, {"Password", Password}};
            var result = _apiDataBrowser.PostData<Dictionary<string, object>>(_apiHostingName, bodyParameters);
        }
    }
}
