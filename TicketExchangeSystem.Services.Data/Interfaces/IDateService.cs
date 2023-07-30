namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    public interface IDateService
    {      
        Task<DateTime> GetFirstDayOfThisWeekAsync( DateTime d);
    }
}
