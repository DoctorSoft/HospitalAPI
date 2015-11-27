using CreateRandomDataTools.Interfaces;
using CreateRandomDataTools.Interfaces.CommonInterfaces;

namespace CreateRandomDataTools.CreationSettings
{
    public class TestCreationSettingsModule : ICreationSettingsModule
    {
        public bool CreateSectionProfiles()
        {
            return true;
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

        public bool CreateDistributiveGroups()
        {
            return false;
        }
    }
}
