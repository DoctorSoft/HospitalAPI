namespace DataBaseModelConfigurations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameEmptyPlaceByTyprStatisticTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EmptyPlaceByTypeStatisticStorageModels", newName: "EmptyPlaceByTypeStatistics");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.EmptyPlaceByTypeStatistics", newName: "EmptyPlaceByTypeStatisticStorageModels");
        }
    }
}
