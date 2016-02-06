namespace Enums.Enums
{
    public enum FunctionIdentityName
    {
        // CommonFunctions >= 1000;
        LogOut = 1000,

        //HospitalUserFunctions 100 <= x < 200
        HospitalUserPrimaryAccess = 100,        // If this function is blocked then all hospital user functions are blocked (Equal function is ShowHomePage for hospital users)
        HospitalUserChangeEmptyPlaces = 101,
        HospitalUserShowMessages = 102,         // If this function is blocked, function 103 is blocked too
        HospitalUserGetWarningMessages = 103,
        HospitalUserSendDistributionMessages = 104,

        //ClinicUserFunctions 200 <= x < 300
        ClinicUserPrimaryAccess = 200,          // If this function is blocked then all clinic user functions are blocked (Equal function is ShowHomePage for clinic users)
        ClinicUserMakeRegistrations = 201,
        ClinicUserBreakRegistrations = 202,
        ClinicUserShowMessages = 203,         // If this function is block, function 204 is blocked too
        ClinicUserGetWarningMessages = 204,

        //BotFunctions 300 <= x < 400
        BotSendAutoWarningNotices = 300,         //Bots have not any access to pages

        //AdministratorFunctions 400 <= x < 500
        AdministratorPrimaryAccess = 400,        // If this function is blocked then all administrator functions are blocked (Equal function is ShowHomePage for administrators)

        //ReviewerFunctions 500 <= x < 600
        ReviewerPrimaryAccess = 500,             // If this function is blocked then all reviewer functions are blocked (Equal function is ShowHomePage for reviewers)
        ReviewerWatchStatistic = 501,

        //ReceptionUserFunctions 600 <= x < 700
        ReceptionUserPrimaryAccess = 600,        // If this function is blocked then all reviewer functions are blocked (Equal function is ShowHomePage for reception users)
        ReceptionUserMarkClients = 601,
        ReceptionUserUnmarkClients = 602
    }
}
