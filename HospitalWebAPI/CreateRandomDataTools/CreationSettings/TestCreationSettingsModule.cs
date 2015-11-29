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

        public bool CreateFunctionalGroups()
        {
            return true;
        }
    }
}
