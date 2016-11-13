namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddHospitalUserSectionAccessTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HospitalUserSectionAccessStorageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBlocked = c.Boolean(nullable: false),
                        HospitalUserId = c.Int(nullable: false),
                        HospitalSectionProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HospitalUsers", t => t.HospitalUserId)
                .ForeignKey("dbo.HospitalSectionProfiles", t => t.HospitalSectionProfileId)
                .Index(t => t.HospitalUserId)
                .Index(t => t.HospitalSectionProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HospitalUserSectionAccessStorageModels", "HospitalSectionProfileId", "dbo.HospitalSectionProfiles");
            DropForeignKey("dbo.HospitalUserSectionAccessStorageModels", "HospitalUserId", "dbo.HospitalUsers");
            DropIndex("dbo.HospitalUserSectionAccessStorageModels", new[] { "HospitalSectionProfileId" });
            DropIndex("dbo.HospitalUserSectionAccessStorageModels", new[] { "HospitalUserId" });
            DropTable("dbo.HospitalUserSectionAccessStorageModels");
        }
    }
}
