using StrategyPattern.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StrategyPattern.Strategies
{
    class FiveDayContractVacationCalculationStrategy : IVacationCalculationStrategy
    {
        public IEnumerable<VacationDayModel> Calculate(IEnumerable<VacationDayModel> vacationDays)
        {
            var workDays = from day in vacationDays
                       where day.DayDate.DayOfWeek != DayOfWeek.Saturday ||
                                day.DayDate.DayOfWeek != DayOfWeek.Sunday
                       select new VacationDayModel
                       {
                           Id = day.Id,
                           DayDate = day.DayDate,
                           VacationType= day.VacationType,
                           CalculatedVacationType = day.VacationType,
                       };
            var weekEnds = from day in vacationDays
                           where day.DayDate.DayOfWeek == DayOfWeek.Saturday ||
                                    day.DayDate.DayOfWeek == DayOfWeek.Sunday
                           select new VacationDayModel
                           {
                               Id = day.Id,
                               DayDate = day.DayDate,
                               VacationType = day.VacationType,
                               CalculatedVacationType = -1,
                           };
            workDays.ToList().AddRange(weekEnds);
            return workDays;
        }
    }
}
