namespace TicketsExchangeSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using System;

    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();
            Category category;

            category = new Category
            {
                Id = 1,
                Name = "Festivals",
            };
            categories.Add(category);

            category = new Category
            {
                Id = 2,
                Name = "Concerts",
            };
            categories.Add(category);

            category = new Category
            {
                Id = 3,
                Name = "Sport events",
            };
            categories.Add(category);

            category = new Category
            {
                Id = 4,
                Name = "Opera and Theatre",
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
