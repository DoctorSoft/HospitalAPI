using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ChangeHospitalRegistrationForNewSectionCommand : AbstractTokenCommand
    {
        public DateTime Date { get; set; }
    }
}
