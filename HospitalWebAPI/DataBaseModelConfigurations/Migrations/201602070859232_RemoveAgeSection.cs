namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAgeSection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Diagnosis", c => c.String(nullable: false));
            DropColumn("dbo.EmptyPlaceByTypeStatistics", "AgeSection");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmptyPlaceByTypeStatistics", "AgeSection", c => c.Int(nullable: false));
            DropColumn("dbo.Reservations", "Diagnosis");
        }
    }
}
