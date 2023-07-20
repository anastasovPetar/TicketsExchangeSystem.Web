namespace TicketsExchangeSystem.Data.Configurations
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

            //builder.HasData(this.GenerateTickets());
        }

        private Ticket[] GenerateTickets() 
        { 
            ICollection<Ticket> tickets = new HashSet<Ticket>();
            Ticket ticket;

            ticket = new Ticket()
            {
                SellerId =  Guid.Parse("9c81219d-35c0-4087-9a97-6bf09134c2a3"),
                Title = "Concert 1",
                Country = "Bulgaria",
                City = "Sofia",
                PlaceOfEvent = "Valis Levski stadium",
                ImageUrl = "https://picsum.photos/id/117/600/400",
                Quantity = 5,
                PricePerTicket = 30.00m,
                EventDate = DateTime.UtcNow
            };
            tickets.Add(ticket);

            return tickets.ToArray();
        }
    }
}
