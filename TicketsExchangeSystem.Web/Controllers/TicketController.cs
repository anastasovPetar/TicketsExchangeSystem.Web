namespace TicketsExchangeSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    [Authorize]
    public class TicketController : Controller
    {
        [AllowAnonymous]
        public IActionResult AllTickets()
        {
            return View();
        }
    }
}
