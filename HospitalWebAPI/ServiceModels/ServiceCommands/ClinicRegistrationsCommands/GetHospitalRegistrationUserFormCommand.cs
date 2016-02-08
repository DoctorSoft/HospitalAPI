using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class GetHospitalRegistrationUserFormCommand : AbstractTokenCommand
    {
        public int SexId { get; set; }
        
        public int HospitalSectionProfileId { get; set; }

        public DateTime Date { get; set; }
    }
}
