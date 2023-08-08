namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    using TicketsExchangeSystem.Web.ViewModels.Category;
    using TicketsExchangeSystem.Web.ViewModels.Currency;
    using static TicketsEchangeSystem.Common.ValidationConstantsForEntities.Ticket;
    using TicketsEchangeSystem.Common;

    public class TicketFormViewModel 
    {
        public TicketFormViewModel()
        {
            Categories = new HashSet<TicketSelectCategoryFormModel>();
            Currencies = new HashSet<TicketSelectCurrencyFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;

        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(PlaceOfEventMaxLength, MinimumLength = PlaceOfEventMinLength)]
        [DisplayName("Place of event")]
        public string PlaceOfEvent { get; set; } = null!;


        [StringLength(AddresslineMaxLength, MinimumLength = AddresslineMinLength)]
        [DisplayName("Address Line 1")]
        public string? Address1 { get; set; }


        [StringLength(AddresslineMaxLength, MinimumLength = AddresslineMinLength)]
        [DisplayName("Address Line 2")]
        public string? Address2 { get; set; }


        [StringLength(ImageUrlMaxLenght)]
        [Display(Name = "Image link")]
        public string? ImageUrl { get; set; }

        [Required]
        [Range(QuantityMinAmount, int.MaxValue, ErrorMessage = "Please enter a valid quantity")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Price per ticket")]
        [Range(typeof(decimal), PricePerTicketMinValue, PricePerTicketMaxValue)]
        public decimal PricePerTicket { get; set; }

        [Required(ErrorMessage ="Please enter the date and time of the event.")]
        [Display(Name = "Event date")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [EventDateValidate(ErrorMessage = "The date and time of the event cannot be before today")]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        public IEnumerable<TicketSelectCategoryFormModel> Categories { get; set; }
        public IEnumerable<TicketSelectCurrencyFormModel> Currencies { get; set; }


    }
}
