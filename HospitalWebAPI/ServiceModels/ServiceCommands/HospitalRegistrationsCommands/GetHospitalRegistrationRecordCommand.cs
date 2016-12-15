using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class GetHospitalRegistrationRecordCommand : AbstractTokenCommand
    {
        public int ReservationId { get; set; }
    }
}
