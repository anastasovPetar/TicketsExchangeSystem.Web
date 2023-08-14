using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TicketsExchangeSystem.Web.ViewModels.Category;
using TicketsExchangeSystem.Web.ViewModels.Currency;

namespace TicketsExchangeSystem.Web.ViewModels.Ticket
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {

            //Categories = new HashSet<TicketSelectCategoryFormModel>();
            //Currencies = new HashSet<TicketSelectCurrencyFormModel>();
        }
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string PlaceOfEvent { get; set; } = null!;

        public string? Addres1 { get; set; }

        public string? Addres2 { get; set; }

        public string? ImageUrl { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerTicket { get; set; }

        

        public DateTime EventDate { get; set; }



        public string Currency { get; set; }
        public string Category { get; set; }
        

        //[Display(Name = "Category")]
        //public int CategoryId { get; set; }

        //[Display(Name = "Currency")]
        //public int CurrencyId { get; set; }

        //public IEnumerable<TicketSelectCategoryFormModel> Categories { get; set; }
        //public IEnumerable<TicketSelectCurrencyFormModel> Currencies { get; set; }

    }
}
