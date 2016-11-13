namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTableFiendsAndRenameIncorrectNames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SettingsItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ClinicHospitalAccesses", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Patients", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Patients", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "Sex", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "PhoneNumber", c => c.String(nullable: false));
            DropTable("dbo.ClinicsRegistrationTime");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClinicsRegistrationTime",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Patients", "PhoneNumber");
            DropColumn("dbo.Patients", "Sex");
            DropColumn("dbo.Patients", "Age");
            DropColumn("dbo.Patients", "LastName");
            DropColumn("dbo.Patients", "FirstName");
            DropColumn("dbo.ClinicHospitalAccesses", "Priority");
            DropTable("dbo.SettingsItems");
        }
    }
}
