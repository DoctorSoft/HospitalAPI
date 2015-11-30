using CreateRandomDataTools.DataCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CreateRandomDataTest
{
    [TestClass]
    public class CheckForNulllModelCreatorsTest
    {
        [TestMethod]
        public void ReturnAdministratorsList()
        {
            var administrator = new AdministratorModelCreator();
            var resoultList = administrator.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnClinicBotsList()
        {
            var clinicBot = new ClinicBotModelCreator();
            var resoultList = clinicBot.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnClinicsList()
        {
            var clinic = new ClinicModelCreator();
            var resoultList = clinic.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnClinicUsersList()
        {
            var clinicUser = new ClinicUserModelCreator();
            var resoultList = clinicUser.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnFunctionalGroupsList()
        {
            var modelCreator = new FunctionalGroupModelCreator();
            var resoultList = modelCreator.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnFunctionsList()
        {
            var function = new FunctionModelCreator();
            var resoultList = function.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnHospitalBotsList()
        {
            var hospitalBot = new HospitalBotModelCreator();
            var resoultList = hospitalBot.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnHospitalsList()
        {
            var hospital = new HospitalModelCreator();
            var resoultList = hospital.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnHospitalUsersList()
        {
            var hospitalUser = new HospitalUserModelCreator();
            var resoultList = hospitalUser.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnSectionModelsList()
        {
            var sectionModel = new SectionModelCreator();
            var resoultList = sectionModel.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnUserFunctionsList()
        {
            var userFunction = new UserFunctionModelCreator();
            var resoultList = userFunction.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnUserTypesList()
        {
            var userType = new UserTypeModelCreator();
            var resoultList = userType.GetList();
            Assert.IsNotNull(resoultList);
        }
    }
}
