using StrategyPattern.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StrategyPattern.Strategies
{
    class PartDayVacationCalculationStrategy : IVacationCalculationStrategy
    {
        public IEnumerable<VacationDayModel> Calculate(IEnumerable<VacationDayModel> vacationDays)
        {
            var workDays = from day in vacationDays
                           where day.DayDate.DayOfWeek != DayOfWeek.Sunday ||
                           day.DayDate.DayOfWeek != DayOfWeek.Saturday||
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
                           where day.DayDate.DayOfWeek != DayOfWeek.Monday ||
                           day.DayDate.DayOfWeek != DayOfWeek.Tuesday||
                           day.DayDate.DayOfWeek != DayOfWeek.Wednesday
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
