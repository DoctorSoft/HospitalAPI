namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHospitalSectionProfileAccess : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClinicHospitalPriorities", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.ClinicHospitalPriorities", "HospitalId", "dbo.Hospitals");
            DropIndex("dbo.ClinicHospitalPriorities", new[] { "ClinicId" });
            DropIndex("dbo.ClinicHospitalPriorities", new[] { "HospitalId" });
            CreateTable(
                "dbo.ClinicUserHospitalSectionProfileAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        ClinicUserId = c.Int(nullable: false),
                        HospitalSectionProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HospitalSectionProfiles", t => t.HospitalSectionProfileId)
                .ForeignKey("dbo.ClinicUsers", t => t.ClinicUserId)
                .Index(t => t.ClinicUserId)
                .Index(t => t.HospitalSectionProfileId);
            
            DropTable("dbo.ClinicHospitalPriorities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClinicHospitalPriorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        ClinicId = c.Int(nullable: false),
                        HospitalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ClinicUserHospitalSectionProfileAccesses", "ClinicUserId", "dbo.ClinicUsers");
            DropForeignKey("dbo.ClinicUserHospitalSectionProfileAccesses", "HospitalSectionProfileId", "dbo.HospitalSectionProfiles");
            DropIndex("dbo.ClinicUserHospitalSectionProfileAccesses", new[] { "HospitalSectionProfileId" });
            DropIndex("dbo.ClinicUserHospitalSectionProfileAccesses", new[] { "ClinicUserId" });
            DropTable("dbo.ClinicUserHospitalSectionProfileAccesses");
            CreateIndex("dbo.ClinicHospitalPriorities", "HospitalId");
            CreateIndex("dbo.ClinicHospitalPriorities", "ClinicId");
            AddForeignKey("dbo.ClinicHospitalPriorities", "HospitalId", "dbo.Hospitals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClinicHospitalPriorities", "ClinicId", "dbo.Clinics", "Id", cascadeDelete: true);
        }
    }
}
