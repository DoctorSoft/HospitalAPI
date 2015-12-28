namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmptyPlaceByTypeStatisticTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "EmptyPlaceStatisticId", "dbo.EmptyPlaceStatistics");
            DropIndex("dbo.Reservations", new[] { "EmptyPlaceStatisticId" });
            CreateTable(
                "dbo.EmptyPlaceByTypeStatisticStorageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sex = c.Int(nullable: false),
                        AgeSection = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        EmptyPlaceStatisticId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmptyPlaceStatistics", t => t.EmptyPlaceStatisticId, cascadeDelete: true)
                .Index(t => t.EmptyPlaceStatisticId);
            
            AddColumn("dbo.Reservations", "EmptyPlaceByTypeStatisticId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "EmptyPlaceByTypeStatisticId");
            AddForeignKey("dbo.Reservations", "EmptyPlaceByTypeStatisticId", "dbo.EmptyPlaceByTypeStatisticStorageModels", "Id", cascadeDelete: true);
            DropColumn("dbo.EmptyPlaceStatistics", "ManRoomCount");
            DropColumn("dbo.EmptyPlaceStatistics", "WomanRoomCount");
            DropColumn("dbo.Reservations", "EmptyPlaceStatisticId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "EmptyPlaceStatisticId", c => c.Int(nullable: false));
            AddColumn("dbo.EmptyPlaceStatistics", "WomanRoomCount", c => c.Int(nullable: false));
            AddColumn("dbo.EmptyPlaceStatistics", "ManRoomCount", c => c.Int(nullable: false));
            DropForeignKey("dbo.EmptyPlaceByTypeStatisticStorageModels", "EmptyPlaceStatisticId", "dbo.EmptyPlaceStatistics");
            DropForeignKey("dbo.Reservations", "EmptyPlaceByTypeStatisticId", "dbo.EmptyPlaceByTypeStatisticStorageModels");
            DropIndex("dbo.Reservations", new[] { "EmptyPlaceByTypeStatisticId" });
            DropIndex("dbo.EmptyPlaceByTypeStatisticStorageModels", new[] { "EmptyPlaceStatisticId" });
            DropColumn("dbo.Reservations", "EmptyPlaceByTypeStatisticId");
            DropTable("dbo.EmptyPlaceByTypeStatisticStorageModels");
            CreateIndex("dbo.Reservations", "EmptyPlaceStatisticId");
            AddForeignKey("dbo.Reservations", "EmptyPlaceStatisticId", "dbo.EmptyPlaceStatistics", "Id", cascadeDelete: true);
        }
    }
}
