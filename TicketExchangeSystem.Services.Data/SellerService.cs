namespace TicketsExchangeSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using TicketsExchangeSystem.Data;
    using TicketsExchangeSystem.Data.Models;
    using TicketsExchangeSystem.Services.Data.Interfaces;
    using TicketsExchangeSystem.Web.ViewModels.Seller;

    public class SellerService : ISellerService
    {
        private readonly TicketsExchangedbContext dbContext;

        public SellerService(TicketsExchangedbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> SellerExistsByUserIdAsync(string userId)
        {
            bool exists = await dbContext
                .Sellers
                .AnyAsync(a => a.UserId.ToString() == userId);

            return exists;
        }

        public async  Task Create(string userId, BecomeSellerFormModel model)
        {
            Seller newSeller = new Seller() 
            { 
                UserId = Guid.Parse(userId),
                Agreed = model.Agreed,
                AgreementDate = DateTime.Now
            };

            await dbContext.Sellers.AddAsync(newSeller);
            await dbContext.SaveChangesAsync();
        }
    }
}
