namespace CreateRandomDataTools.Interfaces.CommonInterfaces
{
    public interface ICreationSettingsModule
    {
        bool CreateSections(); 

        bool CreateHospitals(); 

        bool CreateClinics(); 

        bool CreateFunctions(); 

        bool CreateUserTypes(); 

        bool CreateFunctionalGroups(); 

        bool CreateClinicUsers(); 

        bool CreateHospitalUsers(); 

        bool CreateClinicBots(); 

        bool CreateHospitalBots(); 

        bool CreateAdministrators(); 

        bool CreateUserFunctions(); 

    }
}
