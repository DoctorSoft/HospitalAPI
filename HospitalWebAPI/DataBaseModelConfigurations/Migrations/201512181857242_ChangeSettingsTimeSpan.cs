namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSettingsTimeSpan : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClinicsRegistrationTime", "StartTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.ClinicsRegistrationTime", "EndTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClinicsRegistrationTime", "EndTime", c => c.String(nullable: false));
            AlterColumn("dbo.ClinicsRegistrationTime", "StartTime", c => c.String(nullable: false));
        }
    }
}
