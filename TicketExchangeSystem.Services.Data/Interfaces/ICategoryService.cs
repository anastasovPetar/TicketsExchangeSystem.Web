﻿namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    using TicketsExchangeSystem.Web.ViewModels.Category;
    public interface ICategoryService
    {
        Task<IEnumerable<TicketSelectCategoryFormModel>> GetAllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllCategoriesNamesAsync();
    }
}
