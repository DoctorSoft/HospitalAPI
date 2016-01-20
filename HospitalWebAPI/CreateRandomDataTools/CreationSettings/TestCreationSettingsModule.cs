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

        public bool CreateSettingsItems()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateSettingsItems"]);
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

        public bool CreateReceptionUsers()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateReceptionUsers"]);
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

        public bool CreateClinicHospitalPriorities()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateClinicHospitalPriorities"]);
        }

        public bool CreateHospitalSectionProfiles()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateHospitalSectionProfiles"]);
        }

        public bool CreateMessages()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateMessages"]);
        }

        public bool CreateEmptyPlaceStatistics()
        {
            return bool.Parse(WebConfigurationManager.AppSettings["CreateEmptyPlaceStatistics"]);
        }
    }
}
