using Microsoft.AspNetCore.Identity;

namespace TicketsExchangeSystem.Data.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            this.OrderedTickets = new HashSet<Order>();
        }
        public virtual ICollection<Order> OrderedTickets { get; set; }

    }
}