namespace TicketsExchangeSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Currency
    {
        public Currency()
        {
           Tickets = new HashSet<Ticket>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression("^([A-Z]{3})$", ErrorMessage = "Invalid Currency Name")]
        public string CurrencyCode { get; set; } = null!;


        public  virtual ICollection<Ticket> Tickets { get; set; }

    }
}
