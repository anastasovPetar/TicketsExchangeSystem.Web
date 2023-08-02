namespace TicketsExchangeSystem.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using TicketsExchangeSystem.Data;
    using Web.ViewModels.Currency;
    public class CurrencyService : ICurrencyService
    {
          private readonly TicketsExchangedbContext dbContext;

        public CurrencyService(TicketsExchangedbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<TicketSelectCurrencyFormModel>> GetAllCurrenciesAsync()
        {
            IEnumerable<TicketSelectCurrencyFormModel> currencies = await dbContext
                .Currencies
                .Select(c => new TicketSelectCurrencyFormModel
                {
                    Id = c.Id,
                    CurrencyCode = c.CurrencyCode
                })
                .ToArrayAsync();

            return currencies;
        }
    }
}
