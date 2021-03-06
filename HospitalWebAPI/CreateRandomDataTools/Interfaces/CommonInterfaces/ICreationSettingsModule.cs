﻿namespace CreateRandomDataTools.Interfaces.CommonInterfaces
{
    public interface ICreationSettingsModule
    {
        bool CreateSections(); 

        bool CreateHospitals(); 

        bool CreateClinics();

        bool CreateSettingsItems(); 

        bool CreateFunctions(); 

        bool CreateUserTypes(); 

        bool CreateFunctionalGroups(); 

        bool CreateClinicUsers(); 

        bool CreateHospitalUsers();

        bool CreateReceptionUsers();

        bool CreateBots();

        bool CreateAdministratorsAndReviewers(); 

        bool CreateUserFunctions();

        bool CreateClinicHospitalPriorities();

        bool CreateHospitalSectionProfiles();

        bool CreateMessages();

        bool CreateEmptyPlaceStatistics();

    }
}
