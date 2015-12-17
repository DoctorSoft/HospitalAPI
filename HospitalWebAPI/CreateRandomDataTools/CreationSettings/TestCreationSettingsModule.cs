using System.Web.Configuration;
using CreateRandomDataTools.Interfaces.CommonInterfaces;

namespace CreateRandomDataTools.CreationSettings
{
    public class TestCreationSettingsModule : ICreationSettingsModule
    {
        public bool CreateSections()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateSections"]);
        }

        public bool CreateHospitals()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateHospitals"]);
        }

        public bool CreateClinics()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateClinics"]);
        }

        public bool CreateClinicsRegistrationTime()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateClinicsRegistrationTime"]);
        }

        public bool CreateFunctions()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateFunctions"]);
        }

        public bool CreateUserTypes()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateUserTypes"]);
        }

        public bool CreateFunctionalGroups()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateFunctionalGroups"]);
        }

        public bool CreateClinicUsers()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateClinicUsers"]);
        }

        public bool CreateHospitalUsers()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateHospitalUsers"]);
        }

        public bool CreateBots()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateBots"]);
        }

        public bool CreateAdministratorsAndReviewers()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateAdministratorsAndReviewers"]);
        }

        public bool CreateUserFunctions()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateUserFunctions"]);
        }
    }
}
