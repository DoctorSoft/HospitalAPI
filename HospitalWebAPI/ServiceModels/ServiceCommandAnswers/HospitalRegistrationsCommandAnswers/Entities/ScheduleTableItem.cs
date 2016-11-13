using System;
using System.Collections.Generic;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities
{
    public class ScheduleTableItem
    {
        public Dictionary<DayOfWeek, ScheduleTableCell> Cells { get; set; }
    }
}
