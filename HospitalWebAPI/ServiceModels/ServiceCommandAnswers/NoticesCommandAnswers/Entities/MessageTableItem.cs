using System;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers.Entities
{
    public class MessageTableItem
    {
        public int MessageId { get; set; }

        public string Title { get; set; }

        public DateTime SendDate { get; set; }

        public string AuthorName { get; set; }

        public bool IsRead { get; set; }
    }
}
