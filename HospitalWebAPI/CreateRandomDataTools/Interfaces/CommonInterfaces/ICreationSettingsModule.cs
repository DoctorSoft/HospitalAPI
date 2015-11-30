namespace CreateRandomDataTools.Interfaces.CommonInterfaces
{
    public interface ICreationSettingsModule
    {
        bool CreateSections(); // Section Storage Model (With Section Profile)

        bool CreateHospitals(); // Hospital Storage Model 

        bool CreateClinics(); // Clinic Storage Model

        bool CreateFunctions(); // Function Storage Model

        bool CreateUserTypes(); // User Type Storage Model

        bool CreateFunctionalGroups(); // Functional Group Storage Model (With Group Functions)

        bool CreateClinicUsers(); // Clinic User Storage Model (With User and Account)

        bool CreateHospitalUsers(); // Hospital User Storage Model (With User and Account)

        bool CreateClinicBots(); // User Storage Model

        bool CreateHospitalBots(); // User Storage Model

        bool CreateAdministrators(); // User Storage Model

        bool CreateUserFunctions(); // User Function Storage Model

    }
}
