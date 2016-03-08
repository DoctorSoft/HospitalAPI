using System.Collections.Generic;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers
{
    public class ShowPageToSendDischangeCommandAnswer : AbstractTokenCommandAnswer
    {
        public int ClinicId { get; set; }

        public List<KeyValuePair<int, string>> Clinics { get; set; }
    }
}
