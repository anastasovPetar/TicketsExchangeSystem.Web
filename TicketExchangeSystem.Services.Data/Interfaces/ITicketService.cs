namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    using TicketsExchangeSystem.Web.ViewModels.Home;
    public interface ITicketService
    {
        Task<IEnumerable<TodayViewModel>> GetTodayEventAsync();
        Task<IEnumerable<WeekendViewModel>> GetWeekendEventsAsync();
        Task<IEnumerable<DetailsViewModel>> GetByIdAsysnc(string ticketId);
    }
}
