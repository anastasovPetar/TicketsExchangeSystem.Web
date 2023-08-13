namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    using TicketsExchangeSystem.Web.ViewModels.Ticket;

    public interface ITicketService
    {
        Task<IEnumerable<TodayViewModel>> GetTodayEventAsync();
        Task<IEnumerable<WeekendViewModel>> GetWeekendEventsAsync();
        Task<DetailsViewModel> GetDetailsByIdAsysnc(string ticketId);
        Task<bool> ExistsByIdAsync(string ticketId);
        Task CreateAsync(TicketFormViewModel formViewModel, string sellerId);
    }
}
