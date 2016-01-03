using CreateRandomDataTools.DataCreators;
using DataBaseModelConfigurations.Contexts;
using HelpingTools.CalculationTools;
using HelpingTools.ExtentionTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteServicesTools.Tools;
using Repositories.AnotherRepositories.RemoteAPIRepositories;
using Repositories.DataBaseRepositories.ClinicRepositories;
using Repositories.DataBaseRepositories.FunctionRepositories;
using Repositories.DataBaseRepositories.HospitalRepositories;
using Repositories.DataBaseRepositories.UserRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;

namespace Tests.CreateRandomDataTest
{
    [TestClass]
    public class CheckForNulllModelCreatorsTest
    {
        private readonly TestDataBaseContext context = new TestDataBaseContext();

        [TestMethod]
        public void ReturnAdministratorsList()
        {
            var administrator = new AdministratorAndReviewerModelsCreator(new UserTypeRepository(context), new PasswordHashManager());
            var resoultList = administrator.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnBotsList()
        {
            var bot = new BotModelCreator(new UserTypeRepository(context));
            var resoultList = bot.GetList();
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
            var clinicUser = new ClinicUserModelCreator(new PersonDataAPIRepository(new APIDataBrowser()),
                    new ClinicRepository(context),
                    new UserTypeRepository(context),
                    new PasswordHashManager(),
                    new AccountNameCalculator(new ExtendedRandom()));

            var resoultList = clinicUser.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnFunctionalGroupsList()
        {
            var modelCreator = new FunctionalGroupModelCreator(new UserTypeRepository(context), new FunctionRepository(context));
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
        public void ReturnHospitalsList()
        {
            var hospital = new HospitalModelCreator();
            var resoultList = hospital.GetList();
            Assert.IsNotNull(resoultList);
        }
        [TestMethod]
        public void ReturnHospitalUsersList()
        {
            var hospitalUser = new HospitalUserModelCreator
                (new PersonDataAPIRepository(new APIDataBrowser()),
                    new HospitalRepository(context),
                    new UserTypeRepository(context),
                    new PasswordHashManager(),
                    new AccountNameCalculator(new ExtendedRandom()));

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
            var userFunction = new UserFunctionModelCreator(new UserRepository(context),
                new FunctionalGroupRepository(context));
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
