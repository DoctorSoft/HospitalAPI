using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ChangeHospitalRegistrationForSelectedSectionCommand : AbstractTokenCommand
    {
        public int HospitalProfileId { get; set; }
        public DateTime Date { get; set; }
    }
}
