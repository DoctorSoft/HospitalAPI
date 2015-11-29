namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameReferenceIds : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Sessions", name: "Account_Id", newName: "AccountId");
            RenameColumn(table: "dbo.Messages", name: "UserFrom_Id", newName: "UserFromId");
            RenameColumn(table: "dbo.Messages", name: "UserTo_Id", newName: "UserToId");
            RenameColumn(table: "dbo.UserFunctions", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.ClinicUsers", name: "Clinic_Id", newName: "ClinicId");
            RenameColumn(table: "dbo.Reservations", name: "Clinic_Id", newName: "ClinicId");
            RenameColumn(table: "dbo.Reservations", name: "EmptyPlaceStatistic_Id", newName: "EmptyPlaceStatisticId");
            RenameColumn(table: "dbo.EmptyPlaceStatistics", name: "HospitalSectionProfile_Id", newName: "HospitalSectionProfileId");
            RenameColumn(table: "dbo.HospitalSectionProfiles", name: "Hospital_Id", newName: "HospitalId");
            RenameColumn(table: "dbo.HospitalSectionProfiles", name: "SectionProfile_Id", newName: "SectionProfileId");
            RenameColumn(table: "dbo.HospitalUsers", name: "Hospital_Id", newName: "HospitalId");
            RenameColumn(table: "dbo.SectionProfiles", name: "Section_Id", newName: "SectionId");
            RenameColumn(table: "dbo.UserFunctions", name: "Function_Id", newName: "FunctionId");
            RenameColumn(table: "dbo.GroupFunctions", name: "Function_Id", newName: "FunctionId");
            RenameColumn(table: "dbo.GroupFunctions", name: "DistributiveGroup_Id", newName: "DistributiveGroupId");
            RenameIndex(table: "dbo.Sessions", name: "IX_Account_Id", newName: "IX_AccountId");
            RenameIndex(table: "dbo.ClinicUsers", name: "IX_Clinic_Id", newName: "IX_ClinicId");
            RenameIndex(table: "dbo.Reservations", name: "IX_Clinic_Id", newName: "IX_ClinicId");
            RenameIndex(table: "dbo.Reservations", name: "IX_EmptyPlaceStatistic_Id", newName: "IX_EmptyPlaceStatisticId");
            RenameIndex(table: "dbo.EmptyPlaceStatistics", name: "IX_HospitalSectionProfile_Id", newName: "IX_HospitalSectionProfileId");
            RenameIndex(table: "dbo.HospitalSectionProfiles", name: "IX_Hospital_Id", newName: "IX_HospitalId");
            RenameIndex(table: "dbo.HospitalSectionProfiles", name: "IX_SectionProfile_Id", newName: "IX_SectionProfileId");
            RenameIndex(table: "dbo.HospitalUsers", name: "IX_Hospital_Id", newName: "IX_HospitalId");
            RenameIndex(table: "dbo.SectionProfiles", name: "IX_Section_Id", newName: "IX_SectionId");
            RenameIndex(table: "dbo.Messages", name: "IX_UserTo_Id", newName: "IX_UserToId");
            RenameIndex(table: "dbo.Messages", name: "IX_UserFrom_Id", newName: "IX_UserFromId");
            RenameIndex(table: "dbo.UserFunctions", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.UserFunctions", name: "IX_Function_Id", newName: "IX_FunctionId");
            RenameIndex(table: "dbo.GroupFunctions", name: "IX_DistributiveGroup_Id", newName: "IX_DistributiveGroupId");
            RenameIndex(table: "dbo.GroupFunctions", name: "IX_Function_Id", newName: "IX_FunctionId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.GroupFunctions", name: "IX_FunctionId", newName: "IX_Function_Id");
            RenameIndex(table: "dbo.GroupFunctions", name: "IX_DistributiveGroupId", newName: "IX_DistributiveGroup_Id");
            RenameIndex(table: "dbo.UserFunctions", name: "IX_FunctionId", newName: "IX_Function_Id");
            RenameIndex(table: "dbo.UserFunctions", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Messages", name: "IX_UserFromId", newName: "IX_UserFrom_Id");
            RenameIndex(table: "dbo.Messages", name: "IX_UserToId", newName: "IX_UserTo_Id");
            RenameIndex(table: "dbo.SectionProfiles", name: "IX_SectionId", newName: "IX_Section_Id");
            RenameIndex(table: "dbo.HospitalUsers", name: "IX_HospitalId", newName: "IX_Hospital_Id");
            RenameIndex(table: "dbo.HospitalSectionProfiles", name: "IX_SectionProfileId", newName: "IX_SectionProfile_Id");
            RenameIndex(table: "dbo.HospitalSectionProfiles", name: "IX_HospitalId", newName: "IX_Hospital_Id");
            RenameIndex(table: "dbo.EmptyPlaceStatistics", name: "IX_HospitalSectionProfileId", newName: "IX_HospitalSectionProfile_Id");
            RenameIndex(table: "dbo.Reservations", name: "IX_EmptyPlaceStatisticId", newName: "IX_EmptyPlaceStatistic_Id");
            RenameIndex(table: "dbo.Reservations", name: "IX_ClinicId", newName: "IX_Clinic_Id");
            RenameIndex(table: "dbo.ClinicUsers", name: "IX_ClinicId", newName: "IX_Clinic_Id");
            RenameIndex(table: "dbo.Sessions", name: "IX_AccountId", newName: "IX_Account_Id");
            RenameColumn(table: "dbo.GroupFunctions", name: "DistributiveGroupId", newName: "DistributiveGroup_Id");
            RenameColumn(table: "dbo.GroupFunctions", name: "FunctionId", newName: "Function_Id");
            RenameColumn(table: "dbo.UserFunctions", name: "FunctionId", newName: "Function_Id");
            RenameColumn(table: "dbo.SectionProfiles", name: "SectionId", newName: "Section_Id");
            RenameColumn(table: "dbo.HospitalUsers", name: "HospitalId", newName: "Hospital_Id");
            RenameColumn(table: "dbo.HospitalSectionProfiles", name: "SectionProfileId", newName: "SectionProfile_Id");
            RenameColumn(table: "dbo.HospitalSectionProfiles", name: "HospitalId", newName: "Hospital_Id");
            RenameColumn(table: "dbo.EmptyPlaceStatistics", name: "HospitalSectionProfileId", newName: "HospitalSectionProfile_Id");
            RenameColumn(table: "dbo.Reservations", name: "EmptyPlaceStatisticId", newName: "EmptyPlaceStatistic_Id");
            RenameColumn(table: "dbo.Reservations", name: "ClinicId", newName: "Clinic_Id");
            RenameColumn(table: "dbo.ClinicUsers", name: "ClinicId", newName: "Clinic_Id");
            RenameColumn(table: "dbo.UserFunctions", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Messages", name: "UserToId", newName: "UserTo_Id");
            RenameColumn(table: "dbo.Messages", name: "UserFromId", newName: "UserFrom_Id");
            RenameColumn(table: "dbo.Sessions", name: "AccountId", newName: "Account_Id");
        }
    }
}
