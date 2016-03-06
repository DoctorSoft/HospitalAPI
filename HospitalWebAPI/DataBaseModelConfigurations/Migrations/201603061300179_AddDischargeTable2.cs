namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDischargeTable2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DischargeStorageModels", newName: "Discharges");
            AlterColumn("dbo.Discharges", "Body", c => c.Binary(nullable: false));
            AlterColumn("dbo.Discharges", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Discharges", "MimeType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Discharges", "MimeType", c => c.String());
            AlterColumn("dbo.Discharges", "Name", c => c.String());
            AlterColumn("dbo.Discharges", "Body", c => c.Binary());
            RenameTable(name: "dbo.Discharges", newName: "DischargeStorageModels");
        }
    }
}
