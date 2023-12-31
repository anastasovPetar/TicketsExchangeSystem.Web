﻿namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    using TicketsExchangeSystem.Services.Data.Models.Ticket;
    using TicketsExchangeSystem.Web.ViewModels.Ticket;

    public interface ITicketService
    {
        Task<IEnumerable<TodayViewModel>> GetTodayEventAsync();
        Task<IEnumerable<WeekendViewModel>> GetWeekendEventsAsync();
        Task<TicketDetailsViewModel> GetDetailsByIdAsysnc(string ticketId);
        Task<bool> ExistsByIdAsync(string ticketId);
        Task CreateAsync(TicketFormViewModel formViewModel, string sellerId);
        Task<CustomSearchedAndPaginatedServiceModel> GetAllAsync(CustomTicketQueryModel queryModel);
        Task<IEnumerable<CustomSearchViewModel>> GetAllBySellerIdAsync(string sellerId);
        Task<IEnumerable<CustomSearchViewModel>> GetFavoritesBeUserIdAsync(string userId);
        Task<TicketFormViewModel> GetTicketForEditByIdAsync(string id);
        Task EditByTicketIdAndFormModel(string id,  TicketFormViewModel formViewModel);
        Task<TicketDeleteViewModel> GetTicketForDeleteByIdAsync(string id);
        Task<bool> SoftDeleteByIdAsync(string id, TicketDeleteViewModel model);
    }
}
