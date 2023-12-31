﻿using TicketsExchangeSystem.Web.ViewModels.Seller;

namespace TicketsExchangeSystem.Services.Data.Interfaces
{
    public interface ISellerService
    {
        Task<bool> SellerExistsByUserIdAsync(string userId);

        Task Create(string userId, BecomeSellerFormModel model);

        Task<string?> GetRegisteredSellerIdFromUserIdAsync(string userId);

        Task<bool> IsOwnerOfTicketByUserIdAsync(string userId, string ticketId);
    }
}
