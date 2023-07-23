namespace TicketsExchangeSystem.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using static TicketsEchangeSystem.Common.ValidationConstantsForEntities.Ticket;

    public class Ticket
    {
        public Ticket()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }


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
        public string PlaceOfEvent { get; set; } = null!;


        [MaxLength(AddresslineMaxLength)]
        [DisplayName("Address Line 1")]
        public string? Address1 { get; set; }


        [MaxLength(AddresslineMaxLength)]
        [DisplayName("Address Line 2")]
        public string? Address2 { get; set; }


        [MaxLength(ImageUrlMaxLenght)]
        public string? ImageUrl { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PricePerTicket { get; set; }   

        [Required]
        public DateTime EventDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool isActive { get; set; }



        public Guid SellerId { get; set; }
        public virtual Seller Seller { get; set; } = null!;


        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; } = null!;


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; } 

    }
}
