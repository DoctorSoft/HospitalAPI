using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetClinicRegistrationUserFormCommandAnswer : AbstractTokenCommandAnswer
    {
        public int SexId { get; set; }

        public int SectionProfileId { get; set; }

        public int CurrentHospitalId { get; set; }

        public string Sex { get; set; }

        public string SectionProfile { get; set; }

        public string CurrentHospital { get; set; }

        public DateTime DateValue { get; set; }

        public string Date { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public string Code { get; set; }

        public string PhoneNumber { get; set; }

        public string Diagnosis { get; set; }
    }
}
