using System;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities
{
    public class ClinicBreakRegistrationTableItem
    {
        public int ReservationId { get; set; }

        public string PatientCode { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }

        public string ReservationFormattedDate { get; set; }

        public DateTime ReservationDate { get; set; }

        public string Haspital { get; set; }

        public string SectionProfile { get; set; }
    }
}
