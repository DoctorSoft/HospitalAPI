namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities
{
    public class ScheduleTableCell
    {
        public bool IsStarted { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsThisMonth { get; set; }

        public int Day { get; set; }

        public bool IsThisDate { get; set; }
    }
}
