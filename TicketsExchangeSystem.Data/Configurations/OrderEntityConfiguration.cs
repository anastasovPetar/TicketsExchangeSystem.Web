namespace TicketsExchangeSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    internal class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(o => o.OrderedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(o => o.User)
                .WithMany(ap => ap.OrderedTickets)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
