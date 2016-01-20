using System;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities
{
    public class ClinicScheduleTableCell
    {
        public bool IsBlocked { get; set; }

        public bool IsThisMonth { get; set; }

        public int Day { get; set; }

        public bool IsThisDate { get; set; }

        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}
