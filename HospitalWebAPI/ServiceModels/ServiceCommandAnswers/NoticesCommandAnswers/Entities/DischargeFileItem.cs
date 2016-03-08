using System;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers.Entities
{
    public class DischargeFileItem
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string Hospital { get; set; }

        public string Doctor { get; set; }

        public DateTime SentDate { get; set; }

        public int DischargeId { get; set; }
    }
}
