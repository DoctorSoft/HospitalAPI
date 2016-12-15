namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SetResponsibleClinicField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClinicUsers", "IsDischargeResponsiblePerson", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClinicUsers", "IsDischargeResponsiblePerson");
        }
    }
}
