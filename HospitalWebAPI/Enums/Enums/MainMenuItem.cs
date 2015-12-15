namespace Enums.Enums
{
    public enum MainMenuItem
    {
        // CommonFunctions >= 1000;
        LogOut = 1000,

        //HospitalUserItems 100 <= x < 200
        HospitalUserMainPage = 100,        
        HospitalUserFillEmptyPlacesPage = 101,
        HospitalUserChangeEmptyPlacesPage = 102,
        HospitalUserShowMessagesPage = 103,  

        //ClinicUserFunctions 200 <= x < 300
        ClinicUserMainPage = 200,          
        ClinicUserMakeRegistrationsPage = 201,
        ClinicUserBreakRegistrationsPage = 202,
        ClinicUserShowMessagesPage = 203, 

        //AdministratorFunctions 400 <= x < 500
        AdministratorMainPage = 400,  

        //ReviewerFunctions 500 <= x < 600
        ReviewerMainPage = 500
    }
}
