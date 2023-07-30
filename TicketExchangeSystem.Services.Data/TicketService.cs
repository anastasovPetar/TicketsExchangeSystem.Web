namespace TicketsExchangeSystem.Services.Data
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    using TicketsExchangeSystem.Services.Data.Interfaces;
    using TicketsExchangeSystem.Data;
    using TicketsExchangeSystem.Web.ViewModels.Home;
    using System.Collections.Generic;

    public class TicketService : ITicketService
    {
        private readonly TicketsExchangedbContext dbContext;
        private readonly IDateService dateService;

        public TicketService(TicketsExchangedbContext dbContext, IDateService dateService)
        {
            this.dbContext = dbContext;
            this.dateService = dateService;
        }

       

        public async Task<IEnumerable<TodayViewModel>> GetTodayEventAsync()
        {
            IEnumerable<TodayViewModel> todayEvents = await this.dbContext
                .Tickets
                .Where(t => t.EventDate.Date == DateTime.UtcNow.Date)

                .Select(t => new TodayViewModel()
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Country = t.Country,
                    City = t.City,
                    PlaceOfEvent = t.PlaceOfEvent,
                    ImageUrl = t.ImageUrl,
                    EventDate = t.EventDate
                })
                .ToArrayAsync();

            return todayEvents;
        }


        async Task<IEnumerable<WeekendViewModel>> ITicketService.GetWeekendEventsAsync()
        {
            var startOfWeek = await dateService.GetFirstDayOfThisWeekAsync(DateTime.UtcNow.Date);
            var thisSaturday = startOfWeek.AddDays(5).Date;
            var thisSunday = startOfWeek.AddDays(6).Date;


            IEnumerable<WeekendViewModel> weekendEvents = await dbContext
                .Tickets                
                .Where(t => t.EventDate.Date >= thisSaturday || t.EventDate <= thisSunday)
                .Select(t => new WeekendViewModel()
                 {
                     Id = t.Id.ToString(),
                     Title = t.Title,
                     Country = t.Country,
                     City = t.City,
                     PlaceOfEvent = t.PlaceOfEvent,
                     ImageUrl = t.ImageUrl,
                     EventDate = t.EventDate
                 })
                 .ToListAsync();

                         
            return weekendEvents;
        }

        public async Task<IEnumerable<DetailsViewModel>> GetByIdAsysnc(string ticketId)
        {
            IEnumerable<DetailsViewModel> detailsModel = await this.dbContext
                .Tickets
                .Where(t => t.Id.ToString() == ticketId)
                .Select(t => new DetailsViewModel()
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
                    EventDate = t.EventDate,
                    Category = t.Category.Name
                })
                .ToArrayAsync();

            return detailsModel;
        }
    }
}
