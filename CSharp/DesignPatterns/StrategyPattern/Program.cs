using StrategyPattern.Models;
using StrategyPattern.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var debug = false;
            Dictionary<string, IVacationCalculationStrategy> _myStrategies;
            if (debug)
            {
               _myStrategies = new Dictionary<string, IVacationCalculationStrategy>
             {
                 {"SIXDAYCONTRACT", new SixDayVacationCalculationStrategy() },
                 {"FIVEDAYCONTRACT", new FiveDayContractVacationCalculationStrategy() },
             };
            }
            else
            {
                _myStrategies = new Dictionary<string, IVacationCalculationStrategy>
             {
                 {"SIXDAYCONTRACT", new SixDayVacationCalculationStrategy() },
                 {"FIVEDAYCONTRACT", new FiveDayContractVacationCalculationStrategy() },
                 {"HALFDAYCONTRACT", new PartDayVacationCalculationStrategy() },
                 {"ONEDAYCONTRACT", new OneDayContractCalculationStrategy() },
             };
            }
            var context = new VacationCalculationContext(_myStrategies);
            context.CalculateStrategyBasedOnEmployee(new Employee { ContractType = "HALFDAYCONTRACT" }, new List<VacationDayModel>());
            context.CalculateStrategyBasedOnEmployee(new Employee { ContractType = "ONEDAYCONTRACT" }, new List<VacationDayModel>());
            context.CalculateStrategyBasedOnEmployee(new Employee { ContractType = "SIXDAYCONTRACT" }, new List<VacationDayModel>());
            context.CalculateStrategyBasedOnEmployee(new Employee { ContractType = "HALFDAYCONTRACT" }, new List<VacationDayModel>());
        }
    }
}
