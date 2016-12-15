namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class BindToRegistrator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "ReservatorId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "BehalfReservatorId", c => c.Int());
            CreateIndex("dbo.Reservations", "ReservatorId");
            CreateIndex("dbo.Reservations", "BehalfReservatorId");
            AddForeignKey("dbo.Reservations", "BehalfReservatorId", "dbo.Users", "Id");
            AddForeignKey("dbo.Reservations", "ReservatorId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ReservatorId", "dbo.Users");
            DropForeignKey("dbo.Reservations", "BehalfReservatorId", "dbo.Users");
            DropIndex("dbo.Reservations", new[] { "BehalfReservatorId" });
            DropIndex("dbo.Reservations", new[] { "ReservatorId" });
            DropColumn("dbo.Reservations", "BehalfReservatorId");
            DropColumn("dbo.Reservations", "ReservatorId");
        }
    }
}
