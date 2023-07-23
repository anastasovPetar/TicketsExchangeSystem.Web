namespace TicketsExchangeSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Seller
    {
        public Seller()
        {
            Id = Guid.NewGuid();
            OwnTickets = new HashSet<Ticket>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public bool Agreed { get; set; }


        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;


        public virtual ICollection<Ticket> OwnTickets { get; set; }

        public DateTime AgreementDate { get; set; }
    }
}
