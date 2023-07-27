using TicketsExchangeSystem.Web.ViewModels.Home;

namespace TicketExchangeSystem.Services.Data.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TodayViewModel>> GetTodayEventAsync();
    }
}
