using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ViewRegistrationDetailsMaleCommand : AbstractTokenCommand
    {
        public int HospitalProfileId { get; set; }
        public DateTime Date { get; set; }
    }
}
