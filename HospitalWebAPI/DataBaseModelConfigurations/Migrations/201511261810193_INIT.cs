namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INIT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        Login = c.String(nullable: false),
                        HashedPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        Token = c.Guid(nullable: false),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClinicUsers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Clinic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.Clinic_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Clinic_Id);
            
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ApproveTime = c.DateTime(nullable: false),
                        CancelTime = c.DateTime(),
                        EmptyPlaceStatistic_Id = c.Int(nullable: false),
                        Clinic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmptyPlaceStatistics", t => t.EmptyPlaceStatistic_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clinics", t => t.Clinic_Id, cascadeDelete: true)
                .Index(t => t.EmptyPlaceStatistic_Id)
                .Index(t => t.Clinic_Id);
            
            CreateTable(
                "dbo.EmptyPlaceStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManRoomCount = c.Int(nullable: false),
                        WomanRoomCount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(),
                        HospitalSectionProfile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HospitalSectionProfiles", t => t.HospitalSectionProfile_Id, cascadeDelete: true)
                .Index(t => t.HospitalSectionProfile_Id);
            
            CreateTable(
                "dbo.HospitalSectionProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Hospital_Id = c.Int(nullable: false),
                        SectionProfile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospitals", t => t.Hospital_Id, cascadeDelete: true)
                .ForeignKey("dbo.SectionProfiles", t => t.SectionProfile_Id, cascadeDelete: true)
                .Index(t => t.Hospital_Id)
                .Index(t => t.SectionProfile_Id);
            
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HospitalUsers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Hospital_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospitals", t => t.Hospital_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Hospital_Id);
            
            CreateTable(
                "dbo.SectionProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                        Section_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.Section_Id, cascadeDelete: true)
                .Index(t => t.Section_Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShowStatus = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        MessageType = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        UserFrom_Id = c.Int(nullable: false),
                        UserTo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserFrom_Id)
                .ForeignKey("dbo.Users", t => t.UserTo_Id)
                .Index(t => t.UserFrom_Id)
                .Index(t => t.UserTo_Id);
            
            CreateTable(
                "dbo.UserFunctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Function_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Functions", t => t.Function_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Function_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                        FunctionIdentityName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupFunctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistributiveGroup_Id = c.Int(nullable: false),
                        Function_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DistributiveGroups", t => t.DistributiveGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Functions", t => t.Function_Id, cascadeDelete: true)
                .Index(t => t.DistributiveGroup_Id)
                .Index(t => t.Function_Id);
            
            CreateTable(
                "dbo.DistributiveGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "Id", "dbo.Users");
            DropForeignKey("dbo.UserFunctions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserFunctions", "Function_Id", "dbo.Functions");
            DropForeignKey("dbo.GroupFunctions", "Function_Id", "dbo.Functions");
            DropForeignKey("dbo.GroupFunctions", "DistributiveGroup_Id", "dbo.DistributiveGroups");
            DropForeignKey("dbo.Messages", "UserTo_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserFrom_Id", "dbo.Users");
            DropForeignKey("dbo.HospitalUsers", "Id", "dbo.Users");
            DropForeignKey("dbo.ClinicUsers", "Id", "dbo.Users");
            DropForeignKey("dbo.ClinicUsers", "Clinic_Id", "dbo.Clinics");
            DropForeignKey("dbo.Reservations", "Clinic_Id", "dbo.Clinics");
            DropForeignKey("dbo.Patients", "Id", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "EmptyPlaceStatistic_Id", "dbo.EmptyPlaceStatistics");
            DropForeignKey("dbo.EmptyPlaceStatistics", "HospitalSectionProfile_Id", "dbo.HospitalSectionProfiles");
            DropForeignKey("dbo.HospitalSectionProfiles", "SectionProfile_Id", "dbo.SectionProfiles");
            DropForeignKey("dbo.SectionProfiles", "Section_Id", "dbo.Sections");
            DropForeignKey("dbo.HospitalSectionProfiles", "Hospital_Id", "dbo.Hospitals");
            DropForeignKey("dbo.HospitalUsers", "Hospital_Id", "dbo.Hospitals");
            DropForeignKey("dbo.Sessions", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.GroupFunctions", new[] { "Function_Id" });
            DropIndex("dbo.GroupFunctions", new[] { "DistributiveGroup_Id" });
            DropIndex("dbo.UserFunctions", new[] { "User_Id" });
            DropIndex("dbo.UserFunctions", new[] { "Function_Id" });
            DropIndex("dbo.Messages", new[] { "UserTo_Id" });
            DropIndex("dbo.Messages", new[] { "UserFrom_Id" });
            DropIndex("dbo.Patients", new[] { "Id" });
            DropIndex("dbo.SectionProfiles", new[] { "Section_Id" });
            DropIndex("dbo.HospitalUsers", new[] { "Hospital_Id" });
            DropIndex("dbo.HospitalUsers", new[] { "Id" });
            DropIndex("dbo.HospitalSectionProfiles", new[] { "SectionProfile_Id" });
            DropIndex("dbo.HospitalSectionProfiles", new[] { "Hospital_Id" });
            DropIndex("dbo.EmptyPlaceStatistics", new[] { "HospitalSectionProfile_Id" });
            DropIndex("dbo.Reservations", new[] { "Clinic_Id" });
            DropIndex("dbo.Reservations", new[] { "EmptyPlaceStatistic_Id" });
            DropIndex("dbo.ClinicUsers", new[] { "Clinic_Id" });
            DropIndex("dbo.ClinicUsers", new[] { "Id" });
            DropIndex("dbo.Sessions", new[] { "Account_Id" });
            DropIndex("dbo.Accounts", new[] { "Id" });
            DropTable("dbo.DistributiveGroups");
            DropTable("dbo.GroupFunctions");
            DropTable("dbo.Functions");
            DropTable("dbo.UserFunctions");
            DropTable("dbo.Messages");
            DropTable("dbo.Patients");
            DropTable("dbo.Sections");
            DropTable("dbo.SectionProfiles");
            DropTable("dbo.HospitalUsers");
            DropTable("dbo.Hospitals");
            DropTable("dbo.HospitalSectionProfiles");
            DropTable("dbo.EmptyPlaceStatistics");
            DropTable("dbo.Reservations");
            DropTable("dbo.Clinics");
            DropTable("dbo.ClinicUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Sessions");
            DropTable("dbo.Accounts");
        }
    }
}
