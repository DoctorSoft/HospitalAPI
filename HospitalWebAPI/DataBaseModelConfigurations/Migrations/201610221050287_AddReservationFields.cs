namespace DataBaseModelConfigurations.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddReservationFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "MedicalExaminationResult", c => c.String());
            AddColumn("dbo.Reservations", "MedicalConsultion", c => c.String());
            AddColumn("dbo.Reservations", "ReservationPurpose", c => c.String());
            AddColumn("dbo.Reservations", "OtherInformation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "OtherInformation");
            DropColumn("dbo.Reservations", "ReservationPurpose");
            DropColumn("dbo.Reservations", "MedicalConsultion");
            DropColumn("dbo.Reservations", "MedicalExaminationResult");
        }
    }
}
