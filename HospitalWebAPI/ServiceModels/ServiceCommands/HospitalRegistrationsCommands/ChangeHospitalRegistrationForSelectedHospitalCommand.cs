using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ChangeHospitalRegistrationForSelectedHospitalCommand : AbstractTokenCommand
    {
        public int HospitalProfileId { get; set; }
        public DateTime Date { get; set; }
    }
}
