namespace TicketsExchangeSystem.Web.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TicketsExchangeSystem.Services.Data.Interfaces;
    using Infrastructure.Extentions;
    using ViewModels.Ticket;
    using TicketsExchangeSystem.Services.Data.Models.Ticket;
    using TicketsExchangeSystem.Data.Models;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Components.RenderTree;
    using Microsoft.EntityFrameworkCore;
    using TicketsExchangeSystem.Services.Data;

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


        //TO BE FIXED
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Custom([FromQuery] CustomTicketQueryModel queryModel)
        {
            CustomSearchedAndPaginatedServiceModel serviceModel =
                await ticketService.GetAllAsync(queryModel);

            queryModel.Tickets = serviceModel.Tickets;
            queryModel.TotalTickets = serviceModel.TotalTicketsCount;
            queryModel.Categories = await categoryService.AllCategoriesNamesAsync();

            return View(queryModel);
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

            //string newId; // = string.Empty;
            try
            {
                string? sellerId = await sellerService.GetRegisteredSellerIdFromUserIdAsync(User.GetId()!);
                await ticketService.CreateAsync(model, sellerId!);

                
            }
            catch (Exception)
            {
                notyf.Error("Unexpected error occurred while creating new ticket. Please, try again later.");
                model.Categories = await categoryService.GetAllCategoriesAsync();
                model.Currencies = await currencyService.GetAllCurrenciesAsync();

                return View(model);
            }

            //if (newId == string.Empty)
            //{
            //    notyf.Error("Unexpected error during the creation of the ticket!");
            //}

            notyf.Success("The ticket has been successfully created!");
            return RedirectToAction("Own", "Ticket") ;
        }

        [HttpGet]
        public async Task<IActionResult> Own()
        {
            string userId = this.User.GetId()!;
            bool isSeller = await sellerService.SellerExistsByUserIdAsync(userId!);
            List<CustomSearchViewModel> ownTickets = new List<CustomSearchViewModel>();

            if (isSeller)
            {
                string? sellertId = await sellerService.GetRegisteredSellerIdFromUserIdAsync(userId!);
                ownTickets.AddRange(await ticketService.GetAllBySellerIdAsync(sellertId!));
            }
            else
            {
                notyf.Error("You must be a seller to have own tickets! We are going to redirect you.");

                return RedirectToAction("BecomeSeller", "Seller");
            }
            return View(ownTickets);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            TicketDetailsViewModel viewModel = new TicketDetailsViewModel() { };
            try
            {
                viewModel = await ticketService.GetDetailsByIdAsysnc(id);

            }
            catch (Exception)
            {
                notyf.Error("The ticket does not exists!");

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            TicketFormViewModel viewModel = new TicketFormViewModel() { };

            bool exists = await ticketService.ExistsByIdAsync(id);
            if (!exists)
            {
                notyf.Error("The ticket has expired or does exists!");

                return RedirectToAction("Own", "Ticket");
            }

            bool isSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);
            bool isOwner = await sellerService.IsOwnerOfTicketByUserIdAsync(this.User.GetId()!, id);

            if (!isSeller || !isOwner)
            {
                notyf.Error("You must be a seller to be able to sell tickets! We are going to redirect you.");

                return RedirectToAction("Own", "Ticket");
            }


            try
            {
                viewModel = await ticketService.GetTicketForEditByIdAsync(id);
                viewModel.Categories = await categoryService.GetAllCategoriesAsync();
                viewModel.Currencies = await currencyService.GetAllCurrenciesAsync();

            }
            catch (Exception)
            {
                notyf.Error("The ticket has expired or does exists!");

                return RedirectToAction("Own", "Ticket");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TicketFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    model.Categories = await categoryService.GetAllCategoriesAsync();
                    model.Currencies = await currencyService.GetAllCurrenciesAsync();

                }
                catch (Exception)
                {
                    notyf.Error("Ther is error with database connection. Please try again later.!");

                    return RedirectToAction("Index", "Home");
                }

                return View(model);
            }

            bool exists;

            try
            {
                exists = await ticketService.ExistsByIdAsync(id);
            }
            catch (Exception)
            {
                notyf.Error("Ther is error with database connection. Please try again later.!");

                return RedirectToAction("Index", "Home");
            }


            if (!exists)
            {
                notyf.Error("The ticket has expired or does exists!");

                return RedirectToAction("Own", "Ticket");
            }

            bool isSeller;
            bool isOwner;
            try
            {
                isSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);
                isOwner = await sellerService.IsOwnerOfTicketByUserIdAsync(this.User.GetId()!, id);

            }
            catch (Exception)
            {
                notyf.Error("Wrong ticket or owner!");

                return RedirectToAction("Own", "Ticket");
            }


            if (!isSeller || !isOwner)
            {
                notyf.Error("You must be a seller to be able to sell tickets! We are going to redirect you.");

                return RedirectToAction("Own", "Ticket");
            }

            try
            {
                await ticketService.EditByTicketIdAndFormModel(id, model);
            }
            catch (Exception)
            {
                notyf.Error("Unexpected error ocured while updating the ticket. Plaese try again late.");

                model.Categories = await categoryService.GetAllCategoriesAsync();
                model.Currencies = await currencyService.GetAllCurrenciesAsync();

                return View(model);
            }

            notyf.Success("The Ticked has been updated!");

            return RedirectToAction("Own", "Ticket", new { id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool exists;

            try
            {
                exists = await ticketService.ExistsByIdAsync(id);
            }
            catch (Exception)
            {
                notyf.Error("Ther is error with database connection. Please try again later.!");

                return RedirectToAction("Index", "Home");
            }


            if (!exists)
            {
                notyf.Error("The ticket has expired or does exists!");

                return RedirectToAction("Own", "Ticket");
            }

            bool isSeller;
            bool isOwner;
            try
            {
                isSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);
                isOwner = await sellerService.IsOwnerOfTicketByUserIdAsync(this.User.GetId()!, id);

            }
            catch (Exception)
            {
                notyf.Error("Wrong ticket or owner!");

                return RedirectToAction("Own", "Ticket");
            }


            if (!isSeller || !isOwner)
            {
                notyf.Error("You must be a seller to be able to sell tickets! We are going to redirect you.");

                return RedirectToAction("Own", "Ticket");
            }

            TicketDeleteViewModel model = new TicketDeleteViewModel();
            try
            {
                model = await ticketService.GetTicketForDeleteByIdAsync(id);
                return View(model);
            }
            catch (Exception)
            {
                notyf.Error("Unexpected connection error occurred. Try again later");

                return RedirectToAction("Own", "Ticket");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, TicketDeleteViewModel model)
        {
            bool exists;

            try
            {
                exists = await ticketService.ExistsByIdAsync(id);
            }
            catch (Exception)
            {
                notyf.Error("Ther is error with database connection. Please try again later.!");

                return RedirectToAction("Index", "Home");
            }


            if (!exists)
            {
                notyf.Error("The ticket has expired or does exists!");

                return RedirectToAction("Own", "Ticket");
            }

            bool isSeller;
            bool isOwner;
            try
            {
                isSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);
                isOwner = await sellerService.IsOwnerOfTicketByUserIdAsync(this.User.GetId()!, id);

            }
            catch (Exception)
            {
                notyf.Error("Wrong ticket or owner!");

                return RedirectToAction("Own", "Ticket");
            }


            if (!isSeller || !isOwner)
            {
                notyf.Error("You must be a seller to be able to sell tickets! We are going to redirect you.");

                return RedirectToAction("Own", "Ticket");
            }

            bool softdeleteSuccess = await ticketService.SoftDeleteByIdAsync(id, model);

            if (softdeleteSuccess)
            {
                notyf.Success("Success! The ticket has been deleted.");
            }
            else
            {
                notyf.Error("An Error occured during dele of your ticket.");
            }

            return RedirectToAction("Own", "Ticket");
        }
    }
}
