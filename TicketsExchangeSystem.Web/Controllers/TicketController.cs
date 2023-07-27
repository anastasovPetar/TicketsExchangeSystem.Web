namespace TicketsExchangeSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    [Authorize]
    public class TicketController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> BecomeSeller()
        {
            return View();
        }
    }
}
