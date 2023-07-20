namespace TicketsExchangeSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class TicketsExchangedbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public TicketsExchangedbContext(DbContextOptions<TicketsExchangedbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; } = null!;

        public DbSet<Currency> Currencies { get; set; } = null!;

        public DbSet<Seller> Sellers { get; set; } = null!;

        public DbSet<Status> Statuses { get; set; } = null!;

    }
}