using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetClinicRegistrationUserFormCommandAnswer : AbstractTokenCommandAnswer
    {
        public int SexId { get; set; }

        public string Sex { get; set; }

        public int AgeSectionId { get; set; }

        public string AgeSection { get; set; }

        public int SectionProfileId { get; set; }

        public string SectionProfile { get; set; }

        public int CurrentHospitalId { get; set; }

        public DateTime Date { get; set; }

        public string PatientCode { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }
    }
}
