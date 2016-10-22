namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateReservationFileTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegistrationFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ReservationId = c.Int(nullable: false),
                        File = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistrationFiles", "ReservationId", "dbo.Reservations");
            DropIndex("dbo.RegistrationFiles", new[] { "ReservationId" });
            DropTable("dbo.RegistrationFiles");
        }
    }
}
