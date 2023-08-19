namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    public class WeekendViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string PlaceOfEvent { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public DateTime EventDate { get; set; }

        public int Quantity { get; set; }
    }
}