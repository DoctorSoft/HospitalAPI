using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ShowHospitalRegistrationPlacesByDateCommand : AbstractMessagedCommand
    {
        public DateTime Date { get; set; }
    }
}
