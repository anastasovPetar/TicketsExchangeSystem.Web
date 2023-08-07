namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    using TicketsExchangeSystem.Web.ViewModels.Category;
    using TicketsExchangeSystem.Web.ViewModels.Currency;
    using static TicketsEchangeSystem.Common.ValidationConstantsForEntities.Ticket;
    using TicketsEchangeSystem.Common;

    public class TicketFormViewModel : IValidatableObject
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

        [Required]
        [Display(Name = "Event date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
       // [CustomEventDateValidation]
        public DateTime EventDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        public IEnumerable<TicketSelectCategoryFormModel> Categories { get; set; }
        public IEnumerable<TicketSelectCurrencyFormModel> Currencies { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTime today = DateTime.Now;

            List<ValidationResult> errors = new List<ValidationResult>();
            if (today < EventDate)
            {
                errors.Add(new ValidationResult($"{nameof(EventDate)} needs to be greater than From date.", new List<string> { nameof(today) }));
            }
            return errors;
        }
    }
}
