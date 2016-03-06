namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDischargeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DischargeStorageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.Binary(),
                        Name = c.String(),
                        MimeType = c.String(),
                        MessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.MessageId, cascadeDelete: true)
                .Index(t => t.MessageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DischargeStorageModels", "MessageId", "dbo.Messages");
            DropIndex("dbo.DischargeStorageModels", new[] { "MessageId" });
            DropTable("dbo.DischargeStorageModels");
        }
    }
}
