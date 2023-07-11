
namespace TicketsExchangeSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Seller
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public bool Agreed { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;
    }
}
