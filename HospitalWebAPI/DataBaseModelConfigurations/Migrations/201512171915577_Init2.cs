namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClinicsRegistrationTime",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.String(nullable: false),
                        EndTime = c.String(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClinicsRegistrationTime");
        }
    }
}
