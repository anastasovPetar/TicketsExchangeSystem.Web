namespace TicketsExchangeSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Ticket
    {
        public Ticket()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid SelledId { get; set; }
        public Seller Seller { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;
        public string? Address1 { get; set; } = null!;

        public string? Address2 { get; set; }
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PreicePerTicket { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;

        [Required]
        public DateTime EventDate { get; set; }
        public DateTime CreatedOn { get; set; }



    }
}
