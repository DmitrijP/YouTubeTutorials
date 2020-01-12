using StrategyPattern.Models;
using System.Collections.Generic;

namespace StrategyPattern
{
    interface IVacationCalculationStrategy
    {
        IEnumerable<VacationDayModel> Calculate(IEnumerable<VacationDayModel> vacationDays);
    }
}
