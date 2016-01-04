using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers
{
    public class GetHospitalNoticesMessageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public string Text { get; set; }

        public string Title { get; set; }
    }
}
