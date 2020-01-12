using System;
using StrategyPattern.Models;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern.Strategies
{
    class SixDayVacationCalculationStrategy : IVacationCalculationStrategy
    {
        public IEnumerable<VacationDayModel> Calculate(IEnumerable<VacationDayModel> vacationDays)
        {
            var workDays = from day in vacationDays
                           where day.DayDate.DayOfWeek != DayOfWeek.Sunday   &&
                             day.DayDate.DayOfWeek != DayOfWeek.Saturday ||
                           day.DayDate.DayOfWeek != DayOfWeek.Friday ||
                           day.DayDate.DayOfWeek != DayOfWeek.Thursday
                           select new VacationDayModel
                           {
                               Id = day.Id,
                               DayDate = day.DayDate,
                               VacationType = day.VacationType,
                               CalculatedVacationType = day.VacationType, //Wird vom Kontingent des MA abgezogen
                           };
            var weekEnds = from day in vacationDays
                           where day.DayDate.DayOfWeek == DayOfWeek.Sunday
                           select new VacationDayModel
                           {
                               Id = day.Id,
                               DayDate = day.DayDate,
                               VacationType = day.VacationType,
                               CalculatedVacationType = -1,  //wird nicht vom Kontinget des MA abgezogen
                           };
            workDays.ToList().AddRange(weekEnds);
            return workDays;
        }
    }
}
