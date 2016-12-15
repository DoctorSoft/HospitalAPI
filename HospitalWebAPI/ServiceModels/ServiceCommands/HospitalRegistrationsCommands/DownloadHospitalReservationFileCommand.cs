using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class DownloadHospitalReservationFileCommand : AbstractTokenCommand
    {
        public int HospitalReservationFileId { get; set; }
    }
}
