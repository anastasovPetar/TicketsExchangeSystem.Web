namespace TicketsExchangeSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class SellerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
