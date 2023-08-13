﻿namespace TicketsExchangeSystem.Web.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TicketsExchangeSystem.Services.Data.Interfaces;
    using Infrastructure.Extentions;
    using ViewModels.Ticket;

    [Authorize]
    public class TicketController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ICurrencyService currencyService;
        private readonly ISellerService sellerService;
        private readonly INotyfService notyf;
        private readonly ITicketService ticketService;
        public TicketController(ICategoryService categoryService, 
                                ICurrencyService currencyService,
                                ISellerService sellerService,
                                INotyfService notyf,
                                ITicketService ticketService)
        {
            this.categoryService = categoryService;
            this.currencyService = currencyService;
            this.sellerService = sellerService;
            this.notyf = notyf;
            this.ticketService = ticketService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Today()
        {
            IEnumerable<TodayViewModel> viewModel = await ticketService.GetTodayEventAsync();

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Weekend()
        {
            IEnumerable<WeekendViewModel> viewModel = await ticketService.GetWeekendEventsAsync();

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Custom()
        {
            // return View();
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            bool exists = await ticketService.ExistsByIdAsync(id);

            if (!exists)
            {
                notyf.Error("This ticket does not exists!");

                return RedirectToAction("Index", "Home");
            }

            DetailsViewModel viewModel = await ticketService.GetDetailsByIdAsysnc(id);

            return View(viewModel);
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
        public async Task<IActionResult> Add(TicketFormViewModel model)
        {
            try
            {
                bool isSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

                if (!isSeller)
                {
                    notyf.Error("You must be a seller to be able to sell tickets! We are going to redirect you.");

                    return RedirectToAction("BecomeSeller", "Seller");
                }
            }
            catch (Exception)
            {

                notyf.Error("Unexpected error occurred while accessing the database. Please, try again later.");
            }



            try
            {
                bool categoryExists = await categoryService.ExistsByIdAsync(model.CategoryId);
                if (!categoryExists)
                {
                    notyf.Error("Selected category does not exist!");

                    ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
                }
            }
            catch (Exception)
            {
                notyf.Error("Unexpected error occurred while accessing the database. Please, try again later.");
            }



            try
            {
                bool currecyExists = await currencyService.ExistsByIdAsync(model.CurrencyId);
                if (!currecyExists)
                {
                    notyf.Error("Selected currency does not exist!");

                    ModelState.AddModelError(nameof(model.CurrencyId), "Selected currency does not exist!");
                }
            }
            catch (Exception)
            {
                notyf.Error("Unexpected error occurred while accessing the database. Please, try again later.");
            }




            if (!ModelState.IsValid)
            { 
                try
                {
                    model.Categories = await categoryService.GetAllCategoriesAsync();
                    model.Currencies = await currencyService.GetAllCurrenciesAsync();

                    return View(model);
                }
                catch (Exception)
                {
                    notyf.Error("Unexpected error occurred while accessing the database. Please, try again later.");
                }
            }

            try
            {
                string? sellerId = await sellerService.GetRegisteredSellerIdFromUserIdAsync(User.GetId()!);
                await ticketService.CreateAsync(model, sellerId!);

                notyf.Success("The ticket has been successfully created!");
            }
            catch (Exception)
            {
                notyf.Error("Unexpected error occurred while creating new ticket. Please, try again later.");
                model.Categories = await categoryService.GetAllCategoriesAsync();
                model.Currencies = await currencyService.GetAllCurrenciesAsync();

                return View(model);
            }   
            
            return RedirectToAction("Details", "Ticket");
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return Ok();
        }
    }
}
