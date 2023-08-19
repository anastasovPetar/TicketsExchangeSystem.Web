namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    public class TicketDetailsViewModel : CustomSearchViewModel
    {    
        public string? Addres1 { get; set; }

        public string? Addres2 { get; set; }


        public string ReturnUrl { get; set; } = null!;
    }
}
