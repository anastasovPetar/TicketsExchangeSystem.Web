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

        public async Task<string?> GetRegisteredSellerIdFromUserIdAsync(string userId)
        {
            Seller? seller = await dbContext
                .Sellers
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

            if (seller == null)
            {
                return null;
            }

            return seller.Id.ToString();
        }

        public async Task<bool> IsOwnerOfTicketByUserIdAsync(string userId, string ticketId)
        {
            var existsSeller = await dbContext
               .Sellers
               .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

            if (existsSeller == null)
            {
                return false;
            }
            string sellerIdAsString = existsSeller.Id.ToString();

            var isOwner = await dbContext
                .Tickets
                .Where(t => t.Id.ToString() == ticketId && t.SellerId.ToString() == sellerIdAsString)
                //.Where(t => t.SellerId.ToString() == sellerIdAsString)
                .FirstOrDefaultAsync();

            if (isOwner == null)
            {
                return false;
            }

            return true;
        }
    }
}
