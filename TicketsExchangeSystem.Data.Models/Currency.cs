using System.ComponentModel.DataAnnotations;

namespace TicketsExchangeSystem.Data.Models
{
    public class Currency
    {
        public int Id { get; set; }

        [RegularExpression("^([A-Z]{3})$", ErrorMessage = "Invalid Currency Name")]
        public string CurrencyCode { get; set; } = null!;

        public virtual Ticket? Ticket { get; set; }
    }
}
