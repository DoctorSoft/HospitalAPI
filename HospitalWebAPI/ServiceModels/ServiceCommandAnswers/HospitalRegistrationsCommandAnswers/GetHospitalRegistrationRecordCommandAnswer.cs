using System;
using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetHospitalRegistrationRecordCommandAnswer : AbstractTokenCommandAnswer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Years { get; set; }
        public int Month { get; set; }
        public int Weeks { get; set; }
        public Sex? Sex { get; set; }
        public string Diagnosis { get; set; }
        public string MedicalExaminationResult { get; set; }
        public string MedicalConsultion { get; set; }
        public string ReservationPurpose { get; set; }
        public string OtherInformation { get; set; }
        public string SectionName { get; set; }
        public string ClinicName { get; set; }
        public DateTime Date { get; set; }
        public string DoctorName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int ReservationId { get; set; }
        public int? HospitalReservationFileId { get; set; }
    }
}
