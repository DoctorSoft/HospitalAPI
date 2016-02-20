using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ChangeHospitalRegistrationForSelectedSectionCommand : AbstractTokenCommand
    {
        public int HospitalProfileId { get; set; }
        public string Date { get; set; }
    }
}
