using StrategyPattern.Models;
using StrategyPattern.Strategies;
using System.Collections.Generic;

namespace StrategyPattern
{
    class VacationCalculationContext
    {
        private Dictionary<string, IVacationCalculationStrategy> _myStrategies;

        public VacationCalculationContext(Dictionary<string, IVacationCalculationStrategy> strategies)
        {

            _myStrategies = strategies;
            
        }

        public IEnumerable<VacationDayModel> CalculateStrategyBasedOnEmployee
            (Employee employee, IEnumerable<VacationDayModel> vacationDays)
        {
            var myStrategy = _myStrategies[employee.ContractType];
            var calculatedDays = myStrategy.Calculate(vacationDays);
            return calculatedDays;
        }
    }

    class Employee
    {
        public string ContractType { get; set; } // SIXDAYCONTRACT //FIVEDAYCONTRACT HALFDAYCONTRACT
    }
}
