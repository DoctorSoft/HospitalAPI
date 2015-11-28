using CreateRandomDataTools.DataCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CreateRandomDataTest
{
    [TestClass]
    public class HospitalModelCreatorTest
    {
        [TestMethod]
        public void ReturnHospitalsList()
        {
            var hospital = new HospitalModelCreator();
            var returnHospital = hospital.GetList();
            Assert.IsNotNull(returnHospital);
        }
    }
}
