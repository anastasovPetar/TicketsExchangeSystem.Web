namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    using TicketsExchangeSystem.Services.Data.Models.Ticket;
    using TicketsExchangeSystem.Web.ViewModels.Ticket;

    public interface ITicketService
    {
        Task<IEnumerable<TodayViewModel>> GetTodayEventAsync();
        Task<IEnumerable<WeekendViewModel>> GetWeekendEventsAsync();
        Task<IEnumerable<DetailsViewModel>> GetDetailsByIdAsysnc(string ticketId);
        Task<bool> ExistsByIdAsync(string ticketId);
        Task CreateAsync(TicketFormViewModel formViewModel, string sellerId);
        Task<CustomSearchedAndPaginatedServiceModel> GetAllAsync(CustomTicketQueryModel queryModel);
        Task<IEnumerable<CustomSearchViewModel>> GetAllBySellerIdAsync(string sellerId);
        Task<IEnumerable<CustomSearchViewModel>> GetFavoritesBeUserIdAsync(string userId);
    }
}
