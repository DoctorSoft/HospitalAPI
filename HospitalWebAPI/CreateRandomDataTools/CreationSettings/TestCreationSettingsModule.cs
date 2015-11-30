using CreateRandomDataTools.Interfaces;
using CreateRandomDataTools.Interfaces.CommonInterfaces;

namespace CreateRandomDataTools.CreationSettings
{
    public class TestCreationSettingsModule : ICreationSettingsModule
    {
        public bool CreateSections()
        {
            return true;
        }

        public bool CreateHospitals()
        {
            return true;
        }

        public bool CreateClinics()
        {
            return true;
        }

        public bool CreateFunctions()
        {
            return true;
        }

        public bool CreateUserTypes()
        {
            return true;
        }

        public bool CreateFunctionalGroups()
        {
            return true;
        }

        public bool CreateClinicUsers()
        {
            return true;
        }

        public bool CreateHospitalUsers()
        {
            return true;
        }

        public bool CreateClinicBots()
        {
            return true;
        }

        public bool CreateHospitalBots()
        {
            return true;
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
