using System;

namespace StrategyPattern.Models
{
    class VacationDayModel
    {
        public int Id { get; set; }
        public int VacationType { get; set; }
        public DateTime DayDate { get; set; }
        public int CalculatedVacationType { get; set; }
    }
}
