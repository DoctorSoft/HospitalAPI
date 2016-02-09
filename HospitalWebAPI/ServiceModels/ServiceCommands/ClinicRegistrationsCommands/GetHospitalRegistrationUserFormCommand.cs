using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class GetHospitalRegistrationUserFormCommand : AbstractTokenCommand
    {
        public int SexId { get; set; }

        public string Date { get; set; }

        public int HospitalSectionProfileId { get; set; }

        ////
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public string Code { get; set; }

        public string PhoneNumber { get; set; }

        public string Diagnosis { get; set; }

        public bool? DoesAgree { get; set; }

        public int? ClinicId { get; set; }

        public int? UserId { get; set; }
    }
}
