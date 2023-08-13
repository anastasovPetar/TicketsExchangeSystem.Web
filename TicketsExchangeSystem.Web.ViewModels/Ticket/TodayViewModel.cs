namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel;

    public class TodayViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        [DisplayName("Place")]
        public string PlaceOfEvent { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public DateTime EventDate { get; set; }

    }
}
