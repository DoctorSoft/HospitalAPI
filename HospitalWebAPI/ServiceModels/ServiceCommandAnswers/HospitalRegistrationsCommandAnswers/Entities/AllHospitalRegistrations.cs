using System;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities
{
    public class AllHospitalRegistrations
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Diagnosis { get; set; }
        public string SectionName { get; set; }
        public string ClinicName { get; set; }
        public DateTime Date { get; set; }
        public string DoctorName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int ReservationId { get; set; }
    }
}
