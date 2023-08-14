using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    public class CustomSearchViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;


        [Display(Name ="Address Line 1")]
        public string? Address1 { get; set; }


        [Display(Name = "Address Line 2")]
        public string? Address2 { get; set; }


        [DisplayName("Place")]
        public string PlaceOfEvent { get; set; } = null!;


        [Display(Name ="Image link")]
        public string? ImageUrl { get; set; }

        public int Quantity { get; set; }

        
        public DateTime EventDate { get; set; }

        [Display(Name ="Price per ticket")]
        public decimal PricePerTicket { get; set; }

        public string TicketCurrency { get; set; } = null!;

        public string TicketCategory { get; set; } = null!;
    }
}
