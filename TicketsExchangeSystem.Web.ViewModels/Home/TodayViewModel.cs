namespace TicketsExchangeSystem.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    using static TicketsEchangeSystem.Common.ValidationConstantsForEntities.Ticket;
    public class TodayViewModel
    {
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(PlaceOfEventMaxLength)]
        [DisplayName("Place")]
        public string PlaceOfEvent { get; set; } = null!;

        [MaxLength(ImageUrlMaxLenght)]
        public string? ImageUrl { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PricePerTicket { get; set; }

        public string Currency { get; set; } = null!;

        [Required]
        public DateTime EventDate { get; set; }

    }
}
