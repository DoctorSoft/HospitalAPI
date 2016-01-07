using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers
{
    public class GetClinicMessageByIdCommandAnswer : AbstractTokenCommandAnswer
    {
        public int MessageId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }
    }
}
