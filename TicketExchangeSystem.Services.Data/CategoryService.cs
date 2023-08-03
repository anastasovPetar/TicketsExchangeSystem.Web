﻿namespace TicketsExchangeSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using TicketsExchangeSystem.Data;
    using TicketsExchangeSystem.Services.Data.Interfaces;
    using TicketsExchangeSystem.Web.ViewModels.Category;

    public class CategoryService : ICategoryService
    {
        private readonly TicketsExchangedbContext dbContext;

        public CategoryService(TicketsExchangedbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TicketSelectCategoryFormModel>> GetAllCategoriesAsync()
        {
            IEnumerable<TicketSelectCategoryFormModel> categories = await dbContext
                .Categories
                .Select(c => new TicketSelectCategoryFormModel 
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();

            return categories;
        }
    }
}