using System;
using System.Collections.Generic;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities
{
    public class ClinicScheduleTableItem
    {
        public Dictionary<DayOfWeek, ClinicScheduleTableCell> Cells { get; set; }
    }
}
