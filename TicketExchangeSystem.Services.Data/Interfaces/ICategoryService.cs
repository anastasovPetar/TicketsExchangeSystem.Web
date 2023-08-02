namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    using TicketsExchangeSystem.Web.ViewModels.Category;
    public interface ICategoryService
    {
        Task<IEnumerable<TicketSelectCategoryFormModel>> GetAllCategoriesAsync();
    }
}
