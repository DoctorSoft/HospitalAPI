using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class BreakClinicRegistrationCommand : AbstractTokenCommand
    {
        public int ReservationId { get; set; }
    }
}
