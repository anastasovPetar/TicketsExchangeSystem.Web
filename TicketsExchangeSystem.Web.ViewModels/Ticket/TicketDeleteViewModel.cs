using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    public class TicketDeleteViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        [DisplayName("Place")]
        public string PlaceOfEvent { get; set; } = null!;

        [Display(Name = "Image link")]
        public string? ImageUrl { get; set; }
        public bool isActive { get; set; }
    }
}
