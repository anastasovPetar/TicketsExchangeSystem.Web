namespace TicketsExchangeSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static TicketsEchangeSystem.Common.ValidationConstantsForEntities.Category;
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } = null!;

    }
}
