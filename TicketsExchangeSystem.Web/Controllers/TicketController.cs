namespace TicketsExchangeSystem.Web.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TicketsExchangeSystem.Data.Models;
    using TicketsExchangeSystem.Services.Data.Interfaces;
    using TicketsExchangeSystem.Web.Infrastructure.Extentions;
    using TicketsExchangeSystem.Web.ViewModels.Ticket;

    [Authorize]
    public class TicketController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ICurrencyService currencyService;
        private readonly ISellerService sellerService;
        private readonly INotyfService notyf;
        public TicketController(ICategoryService categoryService, 
                                ICurrencyService currencyService, 
                                ISellerService sellerService,
                                INotyfService notyf)
        {
            this.categoryService = categoryService;
            this.currencyService = currencyService;
            this.sellerService = sellerService;
            this.notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                notyf.Error("You must be a seller to be able to sell tickets! We are going to redirect you.");

                return RedirectToAction("BecomeSeller", "Seller");
            }

            TicketFormViewModel formModel = new TicketFormViewModel()
            {
                Categories = await categoryService.GetAllCategoriesAsync(),
                Currencies = await currencyService.GetAllCurrenciesAsync()
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TicketFormViewModel viewModel)
        {
            bool isSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                notyf.Error("You must be a seller to be able to sell tickets! We are going to redirect you.");

                return RedirectToAction("BecomeSeller", "Seller");
            }

            bool categoryExists = await categoryService.ExistsByIdAsync(viewModel.CategoryId);
            if (!categoryExists)
            {
               // notyf.Error("Selected category does not exist!");

                ModelState.AddModelError(nameof(viewModel.CategoryId), "Selected category does not exist!");

            }

            bool currecyExists = await currencyService.ExistsByIdAsync(viewModel.CurrencyId);
            if (!categoryExists)
            {
                //notyf.Error("Selected category does not exist!");

                ModelState.AddModelError(nameof(viewModel.CurrencyId), "Selected currency does not exist!");

            }


            if (!ModelState.IsValid)
            {
                viewModel.Categories = await categoryService.GetAllCategoriesAsync();
                viewModel.Currencies = await currencyService.GetAllCurrenciesAsync();

                return View(viewModel);
            }
        }
    }
}
