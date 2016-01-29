using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ReceptionMarkingCommands
{
    public class MarkClientAsArrivingCommand : AbstractTokenCommand
    {
        public int ReservationId { get; set; }
    }
}
