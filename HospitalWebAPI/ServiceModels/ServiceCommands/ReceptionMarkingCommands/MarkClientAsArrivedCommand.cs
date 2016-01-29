using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ReceptionMarkingCommands
{
    public class MarkClientAsArrivedCommand : AbstractTokenCommand
    {
        public int ReservationId { get; set; }
    }
}
