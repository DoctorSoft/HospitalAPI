namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.ClinicUsers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ClinicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.ClinicId);
            
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
                "dbo.ClinicHospitalAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        ClinicId = c.Int(nullable: false),
                        HospitalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId, cascadeDelete: true)
                .ForeignKey("dbo.Hospitals", t => t.HospitalId, cascadeDelete: true)
                .Index(t => t.ClinicId)
                .Index(t => t.HospitalId);
            
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
                "dbo.HospitalSectionProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                        HospitalId = c.Int(nullable: false),
                        SectionProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospitals", t => t.HospitalId, cascadeDelete: true)
                .ForeignKey("dbo.SectionProfiles", t => t.SectionProfileId, cascadeDelete: true)
                .Index(t => t.HospitalId)
                .Index(t => t.SectionProfileId);
            
            CreateTable(
                "dbo.EmptyPlaceStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManRoomCount = c.Int(nullable: false),
                        WomanRoomCount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(),
                        HospitalSectionProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HospitalSectionProfiles", t => t.HospitalSectionProfileId, cascadeDelete: true)
                .Index(t => t.HospitalSectionProfileId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ApproveTime = c.DateTime(nullable: false),
                        CancelTime = c.DateTime(),
                        ClinicId = c.Int(nullable: false),
                        EmptyPlaceStatisticId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmptyPlaceStatistics", t => t.EmptyPlaceStatisticId, cascadeDelete: true)
                .ForeignKey("dbo.Clinics", t => t.ClinicId, cascadeDelete: true)
                .Index(t => t.ClinicId)
                .Index(t => t.EmptyPlaceStatisticId);
            
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
                "dbo.SectionProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
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
                "dbo.HospitalUsers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        HospitalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospitals", t => t.HospitalId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.HospitalId);
            
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
                        UserToId = c.Int(nullable: false),
                        UserFromId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserFromId)
                .ForeignKey("dbo.Users", t => t.UserToId)
                .Index(t => t.UserToId)
                .Index(t => t.UserFromId);
            
            CreateTable(
                "dbo.UserFunctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        FunctionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Functions", t => t.FunctionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.FunctionId);
            
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
                        FunctionalGroupId = c.Int(nullable: false),
                        FunctionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FunctionalGroups", t => t.FunctionalGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Functions", t => t.FunctionId, cascadeDelete: true)
                .Index(t => t.FunctionalGroupId)
                .Index(t => t.FunctionId);
            
            CreateTable(
                "dbo.FunctionalGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        Name = c.String(),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "Id", "dbo.Users");
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.UserFunctions", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFunctions", "FunctionId", "dbo.Functions");
            DropForeignKey("dbo.GroupFunctions", "FunctionId", "dbo.Functions");
            DropForeignKey("dbo.FunctionalGroups", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.GroupFunctions", "FunctionalGroupId", "dbo.FunctionalGroups");
            DropForeignKey("dbo.Messages", "UserToId", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserFromId", "dbo.Users");
            DropForeignKey("dbo.HospitalUsers", "Id", "dbo.Users");
            DropForeignKey("dbo.ClinicUsers", "Id", "dbo.Users");
            DropForeignKey("dbo.ClinicUsers", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.Reservations", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.ClinicHospitalAccesses", "HospitalId", "dbo.Hospitals");
            DropForeignKey("dbo.HospitalUsers", "HospitalId", "dbo.Hospitals");
            DropForeignKey("dbo.HospitalSectionProfiles", "SectionProfileId", "dbo.SectionProfiles");
            DropForeignKey("dbo.SectionProfiles", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.HospitalSectionProfiles", "HospitalId", "dbo.Hospitals");
            DropForeignKey("dbo.Reservations", "EmptyPlaceStatisticId", "dbo.EmptyPlaceStatistics");
            DropForeignKey("dbo.Patients", "Id", "dbo.Reservations");
            DropForeignKey("dbo.EmptyPlaceStatistics", "HospitalSectionProfileId", "dbo.HospitalSectionProfiles");
            DropForeignKey("dbo.ClinicHospitalAccesses", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.Sessions", "AccountId", "dbo.Accounts");
            DropIndex("dbo.FunctionalGroups", new[] { "UserTypeId" });
            DropIndex("dbo.GroupFunctions", new[] { "FunctionId" });
            DropIndex("dbo.GroupFunctions", new[] { "FunctionalGroupId" });
            DropIndex("dbo.UserFunctions", new[] { "FunctionId" });
            DropIndex("dbo.UserFunctions", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "UserFromId" });
            DropIndex("dbo.Messages", new[] { "UserToId" });
            DropIndex("dbo.HospitalUsers", new[] { "HospitalId" });
            DropIndex("dbo.HospitalUsers", new[] { "Id" });
            DropIndex("dbo.SectionProfiles", new[] { "SectionId" });
            DropIndex("dbo.Patients", new[] { "Id" });
            DropIndex("dbo.Reservations", new[] { "EmptyPlaceStatisticId" });
            DropIndex("dbo.Reservations", new[] { "ClinicId" });
            DropIndex("dbo.EmptyPlaceStatistics", new[] { "HospitalSectionProfileId" });
            DropIndex("dbo.HospitalSectionProfiles", new[] { "SectionProfileId" });
            DropIndex("dbo.HospitalSectionProfiles", new[] { "HospitalId" });
            DropIndex("dbo.ClinicHospitalAccesses", new[] { "HospitalId" });
            DropIndex("dbo.ClinicHospitalAccesses", new[] { "ClinicId" });
            DropIndex("dbo.ClinicUsers", new[] { "ClinicId" });
            DropIndex("dbo.ClinicUsers", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.Sessions", new[] { "AccountId" });
            DropIndex("dbo.Accounts", new[] { "Id" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.FunctionalGroups");
            DropTable("dbo.GroupFunctions");
            DropTable("dbo.Functions");
            DropTable("dbo.UserFunctions");
            DropTable("dbo.Messages");
            DropTable("dbo.HospitalUsers");
            DropTable("dbo.Sections");
            DropTable("dbo.SectionProfiles");
            DropTable("dbo.Patients");
            DropTable("dbo.Reservations");
            DropTable("dbo.EmptyPlaceStatistics");
            DropTable("dbo.HospitalSectionProfiles");
            DropTable("dbo.Hospitals");
            DropTable("dbo.ClinicHospitalAccesses");
            DropTable("dbo.Clinics");
            DropTable("dbo.ClinicUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Sessions");
            DropTable("dbo.Accounts");
        }
    }
}
