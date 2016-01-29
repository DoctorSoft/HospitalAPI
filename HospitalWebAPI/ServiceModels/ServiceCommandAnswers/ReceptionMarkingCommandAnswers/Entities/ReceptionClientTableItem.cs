namespace ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers.Entities
{
    public class ReceptionClientTableItem
    {
        public string ClientCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ClinicName { get; set; }

        public string SectionName { get; set; }

        public int ReservationId { get; set; }
    }
}
