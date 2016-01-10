using System;
using System.Linq;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;
using Services.Interfaces.HospitalRegistrationsService;

namespace Services.HospitalRegistrationsService
{
    public class HospitalRegistrationsService : IHospitalRegistrationsService
    {
        public GetChangeHospitalRegistrationsPageInformationCommandAnswer GetChangeHospitalRegistrationsPageInformation(
            GetChangeHospitalRegistrationsPageInformationCommand command)
        {
            const int days = 30;
            var now = DateTime.Now;
            var deadLine = now + new TimeSpan(days, 0, 0, 0);

            var startMonday = GetPreviousMonday(now);
            var endMonday = GetPreviousMonday(deadLine);
            var weeks = (endMonday - startMonday).Days / 7 + 1;

            var startSchedule = Enumerable.Range(0, weeks)
                .Select(week => new ScheduleTableItem
                {
                    Cells = Enumerable.Range(0, 7)
                        .ToDictionary(day => (DayOfWeek) day, day => new ScheduleTableCell
                        {
                            IsBlocked = startMonday.AddDays(7 * week + day).Date < now.Date || startMonday.AddDays(7 * week + day).Date > deadLine.Date,
                            Day = startMonday.AddDays(7 * week + day).Day,
                            IsCompleted = false,
                            IsStarted = false,
                            IsThisMonth = startMonday.AddDays(7 * week + day).Month == now.Month,
                            IsThisDate = startMonday.AddDays(7 * week + day).Date == now.Date
                        })
                })
                .ToList();

            return  new GetChangeHospitalRegistrationsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token,
                Schedule = startSchedule
            };
        }

        protected virtual DateTime GetPreviousMonday(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            const DayOfWeek monday = DayOfWeek.Monday;

            var dayDifference = Math.Abs((int) dayOfWeek - (int) monday) % 7;
            var previousMonday = date - new TimeSpan(dayDifference, 0, 0, 0);

            return previousMonday;
        }

        protected virtual DateTime GetNextSunday(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            const DayOfWeek sunday = DayOfWeek.Sunday;

            var dayDifference = Math.Abs((int)dayOfWeek - (int)sunday) % 7;
            var nextSunday = date + new TimeSpan(dayDifference, 0, 0, 0);

            return nextSunday;
        }

        public GetOpenHospitalRegistrationsPageInformationCommandAnswer GetOpenHospitalRegistrationsPageInformation(
            GetOpenHospitalRegistrationsPageInformationCommand command)
        {
            return new GetOpenHospitalRegistrationsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }
    }
}
