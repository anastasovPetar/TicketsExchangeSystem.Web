namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    using TicketsExchangeSystem.Web.ViewModels.Home;
    public interface ITicketService
    {
        Task<IEnumerable<TodayViewModel>> GetTodayEventAsync();
        Task<IEnumerable<WeekendViewModel>> GetWeekendEventsAsync();
        Task<DetailsViewModel> GetDetailsByIdAsysnc(string ticketId);
        Task<bool> ExistsByIdAsync(string ticketId);
    }
}
