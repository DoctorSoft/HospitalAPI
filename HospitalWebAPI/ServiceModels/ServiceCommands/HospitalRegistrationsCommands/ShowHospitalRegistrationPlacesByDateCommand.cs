using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ShowHospitalRegistrationPlacesByDateCommand : AbstractTokenCommand
    {
        public DateTime Date { get; set; }
        public bool ShowModalWindow { get; set; }
    }
}
