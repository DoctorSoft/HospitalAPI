namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAgeFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Years", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "Months", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "Weeks", c => c.Int(nullable: false));
            DropColumn("dbo.Patients", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.Patients", "Weeks");
            DropColumn("dbo.Patients", "Months");
            DropColumn("dbo.Patients", "Years");
        }
    }
}
