namespace Enums.Enums
{
    public enum MainMenuItem
    {
        // CommonFunctions >= 1000;
        LogOut = 1000,

        //HospitalUserItems 100 <= x < 200
        HospitalUserMainPage = 100,        
        HospitalUserChangeEmptyPlacesPage = 101,
        HospitalUserShowMessagesPage = 102,
        HospitalUserSendDistributiveMessagesPage = 104,
        HospitalUserMakeRegistrationsPage = 105,
        HospitalUserWatchRegisteredUsers = 106,

        //ClinicUserFunctions 200 <= x < 300
        ClinicUserMainPage = 200,          
        ClinicUserMakeRegistrationsPage = 201,
        ClinicUserBreakRegistrationsPage = 202,
        ClinicUserShowMessagesPage = 203, 

        //AdministratorFunctions 400 <= x < 500
        AdministratorMainPage = 400,  

        //ReviewerFunctions 500 <= x < 600
        ReviewerMainPage = 500,
        ReviewerShowStatisticPage = 501,

        //ReceptionUserFunctions 600 <= x < 700
        ReceptionUserMainPage = 600,       
        ReceptionUserMarkClientsPage = 601,
        ReceptionUserUnmarkClientsPage = 602
    }
}
