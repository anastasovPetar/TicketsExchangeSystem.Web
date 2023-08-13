namespace TicketsExchangeSystem.Services.Data
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using static TicketsEchangeSystem.Common.GeneralConstants;

    using TicketsExchangeSystem.Services.Data.Interfaces;
    using TicketsExchangeSystem.Data;
    using TicketsExchangeSystem.Web.ViewModels.Home;
    using TicketsExchangeSystem.Data.Models;
    using TicketsExchangeSystem.Web.ViewModels.Ticket;

    public class TicketService : ITicketService
    {
        private readonly TicketsExchangedbContext dbContext;
        private readonly IDateService dateService;
        private readonly DateTime dtNow;

        public TicketService(TicketsExchangedbContext dbContext, IDateService dateService)
        {
            this.dbContext = dbContext;
            this.dateService = dateService;
            this.dtNow = DateTime.Now;
        }



        public async Task<IEnumerable<TodayViewModel>> GetTodayEventAsync()
        {
            IEnumerable<TodayViewModel> todayEvents = await this.dbContext
                .Tickets
                .Where(t => t.isActive)
                .Where(t => t.EventDate.Date == DateTime.Now.Date && t.EventDate > dtNow)

                .Select(t => new TodayViewModel()
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Country = t.Country,
                    City = t.City,
                    PlaceOfEvent = t.PlaceOfEvent,
                    ImageUrl = (t.ImageUrl == null ? noImgPath : t.ImageUrl),
                    EventDate = t.EventDate
                })
                .ToArrayAsync();

            return todayEvents;
        }


        async Task<IEnumerable<WeekendViewModel>> ITicketService.GetWeekendEventsAsync()
        {
            var startOfWeek = await dateService.GetFirstDayOfThisWeekAsync(dtNow);
            var thisSaturday = startOfWeek.AddDays(5);
            var thisSunday = startOfWeek.AddDays(6);

            var weekendStart = thisSaturday;
            var weekendEnd = thisSunday.AddHours(23).AddMinutes(59).AddSeconds(59);
            //var dtNow = DateTime.Now;

           
            IEnumerable<WeekendViewModel> weekendEvents = await dbContext
                .Tickets
                .Where(t => t.isActive)
                .Where(t => t.EventDate >= weekendStart && t.EventDate <= weekendEnd && t.EventDate > dtNow)
                .Select(t => new WeekendViewModel()
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Country = t.Country,
                    City = t.City,
                    PlaceOfEvent = t.PlaceOfEvent,
                    ImageUrl = (t.ImageUrl == null ? noImgPath : t.ImageUrl),
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

        public async Task CreateAsync(TicketFormViewModel formViewModel, string sellerId)
        {

            Ticket ticket = new Ticket()
            {
                Title = formViewModel.Title,
                Country = formViewModel.Country,
                City = formViewModel.City,
                PlaceOfEvent = formViewModel.PlaceOfEvent,
                Address1 = formViewModel.Address1,
                Address2 = formViewModel.Address2,
                ImageUrl = formViewModel.ImageUrl,
                Quantity = formViewModel.Quantity,
                PricePerTicket = formViewModel.PricePerTicket,
                EventDate = (DateTime)formViewModel.EventDate!,
                CreatedOn = DateTime.Now,
                SellerId =  Guid.Parse(sellerId!),
                CurrencyId = formViewModel.CurrencyId,
                CategoryId = formViewModel.CategoryId
            };

            await dbContext.Tickets.AddAsync(ticket);
            await dbContext.SaveChangesAsync();
        }
    }
}
