using System.IO;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class DownloadHospitalReservationFileCommandAnswer : AbstractCommandAnswer
    {
        public Stream File { get; set; }

        public string FileName { get; set; }
    }
}
