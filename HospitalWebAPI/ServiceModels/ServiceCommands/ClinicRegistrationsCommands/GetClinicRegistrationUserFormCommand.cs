using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class GetClinicRegistrationUserFormCommand : AbstractTokenCommand
    {
        public int? SexId { get; set; }

        public int SectionProfileId { get; set; }

        public int CurrentHospitalId { get; set; }

        public DateTime Date { get; set; }

        public string PatientCode { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }
    }
}
