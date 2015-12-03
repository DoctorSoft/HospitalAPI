using CreateRandomDataTools.Interfaces;
using CreateRandomDataTools.Interfaces.CommonInterfaces;

namespace CreateRandomDataTools.CreationSettings
{
    public class TestCreationSettingsModule : ICreationSettingsModule
    {
        public bool CreateSections()
        {
            return false;
        }

        public bool CreateHospitals()
        {
            return false;
        }

        public bool CreateClinics()
        {
            return false;
        }

        public bool CreateFunctions()
        {
            return false;
        }

        public bool CreateUserTypes()
        {
            return false;
        }

        public bool CreateFunctionalGroups()
        {
            return false;
        }

        public bool CreateClinicUsers()
        {
            return false;
        }

        public bool CreateHospitalUsers()
        {
            return false;
        }

        public bool CreateClinicBots()
        {
            return false;
        }

        public bool CreateHospitalBots()
        {
            return false;
        }

        public bool CreateAdministrators()
        {
            return true;
        }

        public bool CreateUserFunctions()
        {
            return true;
        }
    }
}
