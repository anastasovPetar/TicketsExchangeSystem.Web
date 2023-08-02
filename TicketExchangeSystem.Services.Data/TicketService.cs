namespace TicketsExchangeSystem.Services.Data
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    using TicketsExchangeSystem.Services.Data.Interfaces;
    using TicketsExchangeSystem.Data;
    using TicketsExchangeSystem.Web.ViewModels.Home;
    using TicketsExchangeSystem.Data.Models;

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
                .Where(t => t.EventDate.Date == DateTime.Now.Date)

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
            var startOfWeek = await dateService.GetFirstDayOfThisWeekAsync(DateTime.Now.Date);
            var thisSaturday = startOfWeek.AddDays(5).Date;
            var thisSunday = startOfWeek.AddDays(6).Date;


            IEnumerable<WeekendViewModel> weekendEvents = await dbContext
                .Tickets                
                .Where(t => t.EventDate.Date >= thisSaturday)
                .Where(t => t.EventDate.Date <= thisSunday)
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

        public async Task<DetailsViewModel> GetDetailsByIdAsysnc(string ticketId)
        {
            //IEnumerable<DetailsViewModel> detailsModel = await this.dbContext
            //    .Tickets
            //    .Where(t => t.Id.ToString() == ticketId)
            //    .Select(t => new DetailsViewModel()
            //    {
            //        Id = t.Id.ToString(),
            //        Title = t.Title,
            //        Country = t.Country,
            //        City = t.City,
            //        PlaceOfEvent = t.PlaceOfEvent,
            //        ImageUrl = t.ImageUrl,
            //        EventDate = t.EventDate,
            //        Quantity = t.Quantity,
            //        PricePerTicket = t.PricePerTicket,
            //        Currency = t.Currency.CurrencyCode,
            //        Category = t.Category.Name
            //    })
            //    .ToArrayAsync();

            Ticket ticket = await dbContext
                .Tickets
                .Include(t => t.Quantity)
                //.Include(t => t.PricePerTicket)
                //.ThenInclude(c => c.Currency)
                .Include(c => c.Category)
                .OrderBy(t => t.EventDate)
                .FirstAsync(t => t.Id.ToString() == ticketId);

            //Ticket ticket = await dbContext
            //    .Tickets
            //    .FirstAsync(t => t.Id.ToString() == ticketId);

            return new DetailsViewModel()
            {
                Id = ticket.Id.ToString(),
                Title = ticket.Title,
                Country = ticket.Country,
                City = ticket.City,
                PlaceOfEvent = ticket.PlaceOfEvent,
                ImageUrl = ticket.ImageUrl,
                EventDate = ticket.EventDate,
                Quantity = ticket.Quantity,
                PricePerTicket = ticket.PricePerTicket,
                Currency = ticket.Currency.CurrencyCode,
                Category = ticket.Category.Name,
            };
        }

        public async Task<bool> ExistsByIdAsync(string ticketId)
        {
            bool result = await dbContext
                 .Tickets
                 .Where(t => t.isActive)
                 .AnyAsync(t => t.Id.ToString() == ticketId);

            return result;
        }
    }
}
