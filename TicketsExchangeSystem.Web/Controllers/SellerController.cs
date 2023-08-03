namespace TicketsExchangeSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using TicketsExchangeSystem.Services.Data.Interfaces;
    using AspNetCoreHero.ToastNotification.Abstractions;
    using TicketsExchangeSystem.Web.ViewModels.Seller;
    using TicketsExchangeSystem.Web.Infrastructure.Extentions;

    [Authorize]
    public class SellerController : Controller
    {
        private readonly ISellerService sellerService;
        private readonly INotyfService notyf;
        public SellerController(ISellerService sellerService, INotyfService notyf)
        {
            this.sellerService = sellerService;
            this.notyf = notyf;
        }


        [HttpGet]
        public async Task<IActionResult> BecomeSeller() 
        {
            string? userId = this.User.GetId();

            bool alreadySeller = await sellerService.SellerExistsByUserIdAsync(userId);

            if (alreadySeller)
            {
                notyf.Warning("You can already sell your tickets!");

                return RedirectToAction("Add", "Ticket");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BecomeSeller(BecomeSellerFormModel model)
        {
            string? userId = User.GetId();

            bool alreadySeller = await sellerService.SellerExistsByUserIdAsync(userId);

            if (alreadySeller)
            {
                notyf.Warning("You can already sell your tickets!");

                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await sellerService.Create(userId, model);
            }
            catch (Exception)
            {
                notyf.Error("Unexpected error ocured during the registration! Please, try again later or use our contact form!");

                return RedirectToAction("Index", "Home");
            }

            notyf.Success("Now you can sell your tickets");

            // to be changed "AddNew", "Ticket" !!!!!
            return RedirectToAction("Index", "Home");
        }
    }
}
