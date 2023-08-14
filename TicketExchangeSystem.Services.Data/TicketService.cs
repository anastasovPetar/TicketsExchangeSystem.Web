namespace TicketsExchangeSystem.Services.Data
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using static TicketsEchangeSystem.Common.GeneralConstants;

    using TicketsExchangeSystem.Services.Data.Interfaces;
    using TicketsExchangeSystem.Data;
    using TicketsExchangeSystem.Data.Models;
    using TicketsExchangeSystem.Web.ViewModels.Ticket;
    using TicketsExchangeSystem.Services.Data.Models.Ticket;
    using TicketsExchangeSystem.Web.ViewModels.Ticket.Enums;

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
                    EventDate = t.EventDate,
                    //Price = t.PricePerTicket,
                    //CurencyName = t.CurrencyName,
                })
                .ToArrayAsync();

            return todayEvents;
        }


        public async Task<IEnumerable<WeekendViewModel>> GetWeekendEventsAsync()
        {
            var startOfWeek = await dateService.GetFirstDayOfThisWeekAsync(dtNow);
            var thisSaturday = startOfWeek.AddDays(5);
            var thisSunday = startOfWeek.AddDays(6);

            var weekendStart = thisSaturday;
            var weekendEnd = thisSunday.AddHours(23).AddMinutes(59).AddSeconds(59);
            
           
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
                    EventDate = t.EventDate,
                })
                 .ToListAsync();


            return weekendEvents;
        }

        //TO BE FIXED
        public async Task<IEnumerable<DetailsViewModel>> GetDetailsByIdAsysnc(string ticketId)
        {
            IEnumerable<DetailsViewModel> ticket = await dbContext
                .Tickets
                .Where(t => t.isActive)
                .Where(t => t.Id.ToString() == ticketId)
                .Select(t => new DetailsViewModel()
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Country = t.Country,
                    City = t.City,
                    PlaceOfEvent = t.PlaceOfEvent,
                    ImageUrl = (t.ImageUrl == null ? noImgPath : t.ImageUrl),
                    EventDate = t.EventDate,
                    Quantity = t.Quantity,
                    PricePerTicket = t.PricePerTicket,
                    //Currency = t.Currency.CurrencyCode,
                    //Category = t.Category.Name
                })
            .ToListAsync();

            return ticket;           
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

        public async Task<CustomSearchedAndPaginatedServiceModel> GetAllAsync(CustomTicketQueryModel queryModel)
        {
            IQueryable<Ticket> ticketsQuery = dbContext
                .Tickets
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                ticketsQuery = ticketsQuery
                    .Where(t => t.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string search = $"%{queryModel.SearchString.ToLower()}%";
                ticketsQuery = ticketsQuery
                    .Where(t => EF.Functions.Like(t.Title, search) ||
                                EF.Functions.Like(t.Country, search) ||
                                EF.Functions.Like(t.City, search) ||
                                EF.Functions.Like(t.PlaceOfEvent, search) ||
                                EF.Functions.Like(t.Address1, search) ||
                                EF.Functions.Like(t.Address2, search));
            }

            ticketsQuery = queryModel.TicketSorting switch
            {
                TicketSorting.NewestFirst => ticketsQuery.OrderByDescending(t => t.CreatedOn),

                TicketSorting.Country => ticketsQuery.OrderBy(t => t.Country),

                TicketSorting.City => ticketsQuery.OrderBy(t => t.City),

                TicketSorting.QuantityAsc => ticketsQuery.OrderBy(t => t.Quantity),

                TicketSorting.QuantityDesc => ticketsQuery.OrderByDescending(t => t.Quantity)
            };

            IEnumerable<CustomSearchViewModel> customSearchedTicket = await ticketsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.PerPage)
                .Take(queryModel.PerPage)
                .Select(t => new CustomSearchViewModel()
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Country = t.Country,
                    City  = t.City,
                    Address1 = t.Address1,
                    Address2 = t.Address2,
                    PlaceOfEvent = t.PlaceOfEvent,
                    ImageUrl = (t.ImageUrl == null ? noImgPath : t.ImageUrl),
                    Quantity = t.Quantity,
                    EventDate = t.EventDate,
                    PricePerTicket = t.PricePerTicket,
                    TicketCurrency = t.Currency.CurrencyCode,
                    TicketCategory = t.Category.Name
                })
                .ToArrayAsync();

            int totalTickets = ticketsQuery.Count();

            return new CustomSearchedAndPaginatedServiceModel()
            {
                TotalTicketsCount = totalTickets,
                Tickets = customSearchedTicket
            };
        }
    }
}
