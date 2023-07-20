namespace TicketsExchangeSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    internal class CurrencyEntityConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasData(this.GenerateCurrency());
        }

        private Currency[] GenerateCurrency()
        {
            ICollection<Currency> currencies = new HashSet<Currency>();
            Currency currency;

            currency = new Currency
            {
                Id = 1,
                CurrencyCode = "BGN"
            };
            currencies.Add(currency);


            currency = new Currency
            {
                Id = 2,
                CurrencyCode = "EUR"
            };
            currencies.Add(currency);

            return currencies.ToArray();
        }
    }
}
