namespace TicketsExchangeSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Models;
    using System.Reflection;

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

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(TicketsExchangedbContext)) 
                                    ?? Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);
           
            base.OnModelCreating(builder);
        }

    }
}