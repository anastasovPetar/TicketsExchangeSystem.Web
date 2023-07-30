namespace TicketsExchangeSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using TicketsExchangeSystem.Services.Data.Interfaces;
    using ViewModels.Home;

    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ITicketService ticketService;
        
        public HomeController(ITicketService ticketService)
        {
           this.ticketService = ticketService;
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

        [HttpGet]
        public async Task<IActionResult> Today()
        {
            IEnumerable<TodayViewModel> viewModel = await ticketService.GetTodayEventAsync();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Weekend()
        {
            IEnumerable<WeekendViewModel> viewModel = await ticketService.GetWeekendEventsAsync();

            return View(viewModel);
        }


        //[HttpGet]
        //public async Task<IActionResult> Details(string id) 
        //{ 

        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}