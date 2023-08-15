namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    public class TicketDetailsViewModel : CustomSearchViewModel
    {    
        public string? Addres1 { get; set; }

        public string? Addres2 { get; set; }

        public string TicketCategory { get; set; } = null!;      
    }
}
