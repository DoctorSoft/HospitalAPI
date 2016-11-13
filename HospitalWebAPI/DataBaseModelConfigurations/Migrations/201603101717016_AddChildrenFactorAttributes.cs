namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddChildrenFactorAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clinics", "IsForChildren", c => c.Boolean(nullable: false));
            AddColumn("dbo.Hospitals", "IsForChildren", c => c.Boolean(nullable: false));
            AddColumn("dbo.HospitalSectionProfiles", "HasGenderFactor", c => c.Boolean(nullable: false));
            AlterColumn("dbo.EmptyPlaceByTypeStatistics", "Sex", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmptyPlaceByTypeStatistics", "Sex", c => c.Int(nullable: false));
            DropColumn("dbo.HospitalSectionProfiles", "HasGenderFactor");
            DropColumn("dbo.Hospitals", "IsForChildren");
            DropColumn("dbo.Clinics", "IsForChildren");
        }
    }
}
