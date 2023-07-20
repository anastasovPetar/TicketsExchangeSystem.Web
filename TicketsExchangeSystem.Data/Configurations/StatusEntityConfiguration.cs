namespace TicketsExchangeSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    public class StatusEntityConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(GenerateStatusCodes());
        }

        private Status[] GenerateStatusCodes()
        {
            ICollection<Status> statuses = new HashSet<Status>();
            Status status;

            status = new Status()
            {
                Id = 1,
                CurrentStatus = "Awaiting"
            };
            statuses.Add(status);

            status = new Status()
            {
                Id = 2,
                CurrentStatus = "Payed"
            };
            statuses.Add(status);

            status = new Status()
            {
                Id = 3,
                CurrentStatus = "Canceled"
            };
            statuses.Add(status);

            return statuses.ToArray();
        }
    }
}
