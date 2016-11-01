using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class BreakHospitalRegistrationCommand : AbstractTokenCommand
    {
        public int EmptyPlaceByTypeStatisticId { get; set; }
        public int? FullInformation { get; set; }
        public int HospitalProfileId { get; set; }
        public string Date { get; set; }
        public int ReservationId { get; set; }

        public string Cause { get; set; }
    }
}
