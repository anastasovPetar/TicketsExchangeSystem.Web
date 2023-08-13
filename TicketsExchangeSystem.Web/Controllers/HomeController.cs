namespace TicketsExchangeSystem.Web.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    //using TicketsExchangeSystem.Services.Data.Interfaces;
    using ViewModels.Home;

    [AllowAnonymous]
    public class HomeController : Controller
    {
        //private readonly ITicketService ticketService;
        private readonly INotyfService notyf;


        public HomeController(INotyfService notyf)
        {
           //this.ticketService = ticketService;
           this.notyf = notyf;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}