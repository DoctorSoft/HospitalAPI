using System.IO;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.NoticesCommands
{
    public class SaveDischargeCommand : AbstractTokenCommand
    {
        public string FileName { get; set; }

        public Stream File { get; set; }

        public int Size { get; set; }

        public string ContentType { get; set; }

        public int ClinicId { get; set; }
    }
}
