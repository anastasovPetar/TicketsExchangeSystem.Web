namespace TicketsExchangeSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using TicketExchangeSystem.Services.Data.Interfaces;
    using TicketsExchangeSystem.Data;
    using TicketsExchangeSystem.Web.ViewModels.Home;

    internal class TicketService : ITicketService
    {       
        private readonly TicketsExchangedbContext dbContext;

        public TicketService(TicketsExchangedbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<TodayViewModel>> GetTodayEventAsync()
        {
            IEnumerable<TodayViewModel> todayEvents = await this.dbContext
                .Tickets
                .Where(t => t.CreatedOn == DateTime.UtcNow.Date)
                .Select(t => new TodayViewModel()
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Country = t.Country,
                    City = t.City,
                    PlaceOfEvent = t.PlaceOfEvent,
                    ImageUrl = t.ImageUrl,
                    Quantity = t.Quantity,
                    PricePerTicket = t.PricePerTicket,
                    Currency = t.Currency.CurrencyCode,
                    EventDate = t.EventDate
                })
                .ToArrayAsync();

            return todayEvents;
        }
    }
}
