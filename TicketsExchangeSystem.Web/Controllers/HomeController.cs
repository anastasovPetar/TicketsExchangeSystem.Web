namespace TicketsExchangeSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using TicketExchangeSystem.Services.Data.Interfaces;
    using ViewModels.Home;

    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ITicketService ticketService;
        
        public HomeController(ITicketService ticketService)
        {
           this.ticketService = ticketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Today()
        {
            IEnumerable<TodayViewModel> viewModel = await ticketService.GetTodayEventAsync();

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}