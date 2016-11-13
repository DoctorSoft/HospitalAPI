namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RenamePriorityTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ClinicHospitalAccesses", newName: "ClinicHospitalPriorities");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ClinicHospitalPriorities", newName: "ClinicHospitalAccesses");
        }
    }
}
