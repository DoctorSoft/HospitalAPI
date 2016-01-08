using System;
using System.Collections.Generic;
using Enums.Enums;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities
{
    public class ScheduleTableItem
    {
        public Dictionary<DayOfWeek, ScheduleTableCell> Cells { get; set; }
    }
}
