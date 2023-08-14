using TicketsExchangeSystem.Web.ViewModels.Ticket;

namespace TicketsExchangeSystem.Services.Data.Models.Ticket
{
    public class CustomSearchedAndPaginatedServiceModel
    {
        public CustomSearchedAndPaginatedServiceModel()
        {
            this.Tickets = new HashSet<CustomSearchViewModel>();
        }
        public int TotalTicketsCount { get; set; }

        public IEnumerable<CustomSearchViewModel> Tickets { get; set; }
    }
}
