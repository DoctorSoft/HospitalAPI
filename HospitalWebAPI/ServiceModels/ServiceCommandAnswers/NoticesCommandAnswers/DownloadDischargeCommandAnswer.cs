using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers
{
    public class DownloadDischargeCommandAnswer : AbstractTokenCommandAnswer
    {
        public byte[] Body { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }
    }
}
