﻿namespace TicketsExchangeSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    internal class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .Property(t => t.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(t => t.isActive)
                .HasDefaultValue(true);

            builder
                .HasOne(t => t.Currency)
                .WithMany(cu => cu.Tickets)
                .HasForeignKey(t => t.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Seller)
                .WithMany(s => s.OwnTickets)
                .HasForeignKey(t => t.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Category)
                .WithMany (c => c.Tickets)
                .HasForeignKey (t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Order)
                .WithMany(o => o.OrderedTickets)
                .HasForeignKey(t => t.OrderId)
                .OnDelete (DeleteBehavior.Restrict);

            builder.HasData(this.GenerateTickets());
        }

        private Ticket[] GenerateTickets() 
        { 
            ICollection<Ticket> tickets = new HashSet<Ticket>();
            Ticket ticket;

            ticket = new Ticket()
            {                
                Title = "Concert 1",
                Country = "Bulgaria",
                City = "Sofia",
                PlaceOfEvent = "Valis Levski stadium",
                ImageUrl = "https://picsum.photos/id/117/600/400",
                Quantity = 5,
                PricePerTicket = 30.00m,
                EventDate = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                isActive = true,
                SellerId = Guid.Parse("A061BA74-B8BE-4802-AC71-A5A7FA839815"), //SellerID
                CurrencyId = 1,
                CategoryId = 2
            };
            tickets.Add(ticket);

            ticket = new Ticket()
            {
                Title = "Concert 2",
                Country = "Bulgaria",
                City = "Sofia",
                PlaceOfEvent = "Stadium Junak",
                ImageUrl = "https://picsum.photos/id/117/600/400",
                Quantity = 2,
                PricePerTicket = 20.00m,
                EventDate = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow,
                isActive = true,
                SellerId = Guid.Parse("A061BA74-B8BE-4802-AC71-A5A7FA839815"),
                CurrencyId = 1,
                CategoryId = 2
            };
            tickets.Add(ticket);

            ticket = new Ticket()
            {
                Title = "ORPHEUS",
                Country = "Bulgaria",
                City = "Sofia",
                PlaceOfEvent = "National Theatre Ivan Vazov",
                ImageUrl = "https://picsum.photos/id/117/600/400",
                Quantity = 2,
                PricePerTicket = 25.00m,
                EventDate = DateTime.UtcNow.AddDays(2),
                CreatedOn = DateTime.UtcNow,
                isActive = true,
                SellerId = Guid.Parse("A061BA74-B8BE-4802-AC71-A5A7FA839815"),
                CurrencyId = 1,
                CategoryId = 4
            };
            tickets.Add(ticket);

            return tickets.ToArray();
        }
    }
}
