using CreateRandomDataTools.DataCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CreateRandomDataTest
{
    [TestClass]
    public class CheckForNulllModelCreatorsTest
    {
        [TestMethod]
        public void ReturnClinicsList()
        {
            var clinic = new ClinicModelCreator();
            var clinicsList = clinic.GetList();
            Assert.IsNotNull(clinicsList);
        }
        [TestMethod]
        public void ReturnFunctionalGroupsList()
        {
            var modelCreator = new FunctionalGroupModelCreator();
            var models = modelCreator.GetList();
            Assert.IsNotNull(models);
        }
        [TestMethod]
        public void ReturnFunctionsList()
        {
            var function = new FunctionModelCreator();
            var functionsList = function.GetList();
            Assert.IsNotNull(functionsList);
        }
        [TestMethod]
        public void ReturnHospitalsList()
        {
            var hospital = new HospitalModelCreator();
            var hospitalsList = hospital.GetList();
            Assert.IsNotNull(hospitalsList);
        }
        [TestMethod]
        public void ReturnSectionModelsList()
        {
            var sectionModel = new SectionModelCreator();
            var sectionModelsList= sectionModel.GetList();
            Assert.IsNotNull(sectionModelsList);
        }
    }
}
