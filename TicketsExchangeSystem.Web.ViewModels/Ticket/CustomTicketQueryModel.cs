namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    using static TicketsEchangeSystem.Common.GeneralConstants;

    public class CustomTicketQueryModel
    {
        public CustomTicketQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.PerPage = ItemsPerPage;
            this.Categories = new HashSet<string>();
            this.Tickets = new HashSet<CustomSearchViewModel>();
        }


        //Caterogy to order by
        public string? Category { get; set; }

        [Display(Name ="Search for")]
        public string? SearchString { get; set; }

        [Display(Name ="Sort by")]
        public TicketSorting TicketSorting { get; set; }

        public int CurrentPage { get; set; }

        public int TotalTickets { get; set; }

        [Display(Name = "Items per page")]
        public int PerPage { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<CustomSearchViewModel> Tickets { get; set; }
    }
}
