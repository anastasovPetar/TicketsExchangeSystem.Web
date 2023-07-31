namespace TicketsExchangeSystem.Web.ViewModels.Home
{
    public class DetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string PlaceOfEvent { get; set; } = null!;
        
        public string? Addres1 { get; set; }

        public string? Addres2 { get; set; }

        public string? ImageUrl { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerTicket { get; set; }

        public string Currency { get; set; } = null!;

        public DateTime EventDate { get; set; }

        public string Category { get; set; } = null!;
    }
}
