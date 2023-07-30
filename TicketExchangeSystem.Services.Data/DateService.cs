using TicketsExchangeSystem.Services.Data.Interfaces;

namespace TicketsExchangeSystem.Services.Data
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    public class DateService : IDateService
    {        
        public async Task<DateTime> GetFirstDayOfThisWeekAsync(DateTime d)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;

            var first = (int)ci.DateTimeFormat.FirstDayOfWeek;
            var current =  (int)d.DayOfWeek;

            var result = await Task.FromResult(first <= current
                ? d.AddDays(-1 * (current - first))
                : d.AddDays(first - current - 7));
            
            return result;
        }
    }
}
