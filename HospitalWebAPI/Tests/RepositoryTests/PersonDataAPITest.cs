using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteServicesTools.Tools;
using Repositories.AnotherRepositories.RemoteAPIRepositories;

namespace Tests.RepositoryTests
{
    [TestClass]
    public class PersonDataAPITest
    {
        [TestMethod]
        public void GetData()
        {
            var repository = new PersonDataAPIRepository(new APIDataBrowser());
            const int Id = 1;


            var list = repository.GetModels().ToList();
            var actual = repository.GetModelById(Id);

            var excepted = list.FirstOrDefault(model => model.Id == Id);

            Assert.AreEqual(excepted.FirstName, actual.FirstName);
        }
    }
}
