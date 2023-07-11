using Microsoft.AspNetCore.Identity;

namespace TicketsExchangeSystem.Data.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.PurchasedTickets = new HashSet<Ticket>();
        }
        public virtual ICollection<Ticket> PurchasedTickets { get; set; }

    }
}