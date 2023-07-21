namespace TicketsExchangeSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Order
    {

        public Order()
        {
            Id = Guid.NewGuid();
            OrderedTickets = new HashSet<Ticket>();
        }

        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
       
        public DateTime OrderedOn { get; set; }


        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;


        public ICollection<Ticket> OrderedTickets { get; set; } = null!;

    }
}
