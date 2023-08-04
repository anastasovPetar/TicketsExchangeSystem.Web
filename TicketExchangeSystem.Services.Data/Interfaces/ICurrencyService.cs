namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    using TicketsExchangeSystem.Web.ViewModels.Currency;
    public interface ICurrencyService
    {
        Task<IEnumerable<TicketSelectCurrencyFormModel>> GetAllCurrenciesAsync();
        Task<bool> ExistsByIdAsync(int currencyId);
    }
}
