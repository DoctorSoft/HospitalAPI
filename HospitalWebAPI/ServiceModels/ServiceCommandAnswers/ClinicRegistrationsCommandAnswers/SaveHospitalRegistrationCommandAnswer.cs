using System.Collections.Generic;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class SaveHospitalRegistrationCommandAnswer : AbstractMessagedCommandAnswer
    {
        public int? SexId { get; set; }

        public int HospitalSectionProfileId { get; set; }

        public string HospitalSectionProfile { get; set; }

        public string Sex { get; set; }

        public string Date { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Years { get; set; }

        public int? Months { get; set; }

        public int? Weeks { get; set; }

        public string Code { get; set; }

        public string PhoneNumber { get; set; }

        public string Diagnosis { get; set; }

        public bool DoesAgree { get; set; }

        public int ClinicId { get; set; }

        public int UserId { get; set; }

        public List<KeyValuePair<int, string>> Clinics { get; set; }

        public List<KeyValuePair<int, string>> Users { get; set; }
    }
}
