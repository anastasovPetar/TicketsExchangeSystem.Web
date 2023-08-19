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
    using static TicketsEchangeSystem.Common.ValidationConstantsForEntities;
    using Ticket = TicketsExchangeSystem.Data.Models.Ticket;

    public class TicketService : ITicketService
    {
        private readonly TicketsExchangedbContext dbContext;
        private readonly IDateService dateService;
        private readonly DateTime dtNow;

        public TicketService(TicketsExchangedbContext dbContext, IDateService dateService)
        {
            this.dbContext = dbContext;
            this.dateService = dateService;
            dtNow = DateTime.Now;
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
                    Quantity = t.Quantity
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
                    Quantity = t.Quantity
                })
                 .ToListAsync();


            return weekendEvents;
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
                SellerId = Guid.Parse(sellerId!),
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
                                EF.Functions.Like(t.Address1!, search) ||
                                EF.Functions.Like(t.Address2!, search));
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
                .Where(t => t.isActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.PerPage)
                .Take(queryModel.PerPage)
                .Select(t => new CustomSearchViewModel()
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Country = t.Country,
                    City = t.City,
                    PlaceOfEvent = t.PlaceOfEvent,
                    ImageUrl = (t.ImageUrl == null ? noImgPath : t.ImageUrl),
                    Quantity = t.Quantity,
                    EventDate = t.EventDate,
                    PricePerTicket = t.PricePerTicket,
                    TicketCurrency = t.Currency.CurrencyCode,
                    TicketCategory = t.Category.Name,
                    SellerId = t.SellerId.ToString()
                })
                .ToArrayAsync();

            int totalTickets = ticketsQuery.Count();

            return new CustomSearchedAndPaginatedServiceModel()
            {
                TotalTicketsCount = totalTickets,
                Tickets = customSearchedTicket
            };
        }

        public async Task<IEnumerable<CustomSearchViewModel>> GetAllBySellerIdAsync(string sellerid)
        {
            IEnumerable<CustomSearchViewModel> allSellersTickets = await dbContext
                .Tickets
                .Where(t => t.isActive)
                .Where(t => t.SellerId.ToString() == sellerid)
                .Select(t => new CustomSearchViewModel
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    Country = t.Country,
                    City = t.City,
                    PlaceOfEvent = t.PlaceOfEvent,
                    ImageUrl = (t.ImageUrl == null ? noImgPath : t.ImageUrl),
                    Quantity = t.Quantity,
                    EventDate = t.EventDate,
                    PricePerTicket = t.PricePerTicket,
                    TicketCurrency = t.Currency.CurrencyCode
                })
                .ToArrayAsync();

            return allSellersTickets;
        }

        public Task<IEnumerable<CustomSearchViewModel>> GetFavoritesBeUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketDetailsViewModel> GetDetailsByIdAsysnc(string ticketId)
        {
            Ticket ticket = await dbContext
                .Tickets
                .Include(t => t.Currency)
                .Include(t => t.Category)
                .Where(t => t.isActive)
                .FirstAsync(t => t.Id.ToString() == ticketId);

            return new TicketDetailsViewModel()
            {
                Id = ticket.Id.ToString(),
                Title = ticket.Title,
                Country = ticket.Country,
                City = ticket.City,
                Addres1 = ticket.Address1,
                Addres2 = ticket.Address2,
                PlaceOfEvent = ticket.PlaceOfEvent,
                ImageUrl = (ticket.ImageUrl == null ? noImgPath : ticket.ImageUrl),
                Quantity = ticket.Quantity,
                EventDate = ticket.EventDate,
                PricePerTicket = ticket.PricePerTicket,
                TicketCurrency = ticket.Currency.CurrencyCode,
                TicketCategory = ticket.Category.Name
            };
        }

        public async Task<TicketFormViewModel> GetTicketForEditByIdAsync(string id)
        {
            var ticket = await dbContext
                .Tickets
                .Include(t => t.Currency)
                .Include(t => t.Category)
                .Where(t => t.isActive)
                .FirstAsync(t => t.Id.ToString() == id);

            return new TicketFormViewModel()
            {
                Title = ticket.Title,
                Country = ticket.Country,
                City = ticket.City,
                Address1 = ticket.Address1,
                Address2 = ticket.Address2,
                PlaceOfEvent = ticket.PlaceOfEvent,
                ImageUrl = (ticket.ImageUrl == null ? noImgPath : ticket.ImageUrl),
                Quantity = ticket.Quantity,
                EventDate = ticket.EventDate,
                PricePerTicket = ticket.PricePerTicket,
                CurrencyId = ticket.CurrencyId,
                CategoryId = ticket.CategoryId
            };
        }

        public async Task EditByTicketIdAndFormModel(string id, TicketFormViewModel formViewModel)
        {
            Ticket ticket = await dbContext
                .Tickets
                .Where(t => t.isActive)
                .FirstAsync(t => t.Id.ToString() == id);

            ticket.Title = formViewModel.Title;
            ticket.Country = formViewModel.Country;
            ticket.City = formViewModel.City;
            ticket.Address1 = formViewModel.Address1;
            ticket.Address2 = formViewModel.Address2;
            ticket.PlaceOfEvent = formViewModel.PlaceOfEvent;
            ticket.ImageUrl = formViewModel.ImageUrl;
            ticket.Quantity = formViewModel.Quantity;
            ticket.EventDate = (DateTime)formViewModel.EventDate!;
            ticket.PricePerTicket = formViewModel.PricePerTicket;
            ticket.CurrencyId = formViewModel.CurrencyId;
            ticket.CategoryId = formViewModel.CategoryId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<TicketDeleteViewModel> GetTicketForDeleteByIdAsync(string id)
        {
            Ticket ticket = await dbContext.
                Tickets
                .Where(t => t.isActive)
                .FirstAsync(t => t.Id.ToString() == id);

            return new TicketDeleteViewModel 
            {
                Id = ticket.Id.ToString(),
                Title = ticket.Title,
                Country = ticket.Country,
                City = ticket.City,
                ImageUrl = (ticket.ImageUrl == null ? noImgPath : ticket.ImageUrl),
                PlaceOfEvent = ticket.PlaceOfEvent
            };
        }

        public async Task<bool> SoftDeleteByIdAsync(string id, TicketDeleteViewModel model)
        {
            Ticket ticket = await dbContext
                .Tickets
                .Where(t => t.isActive)
                .FirstAsync(t => t.Id.ToString() == id);


            ticket.isActive = false;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
