using EvaFloraStore.Data;
using Microsoft.EntityFrameworkCore;

namespace EvaFloraStore.Models.SeedData
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder applicationBuilder)
        {
            ItemsDbContext context = applicationBuilder.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ItemsDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Products.Any())
            {
                context.AddRange(
                new Product
                {
                    Name = "Мед, 1000 мл",
                    Description = "Баночка меда с нашей пасеки",
                    Category = new Category { Name = "Продукты пчеловодства" },
                    Price = 950M,

                },
                new Product
                {
                    Name = "Набор подарочный весенний",
                    Description = "Подарочный набор",
                    Category = new Category { Name = "Подарочные наборы" },
                    Price = 4500M,

                });
                context.SaveChanges();

                context.AddRange(
                new Product
                {
                    Name = "Мед, 250 мл",
                    Description = "Баночка меда с нашей пасеки",
                    Category = context.Categories.FirstOrDefault(x => x.Name == "Продукты пчеловодства"),
                    Price = 300M,
                },
                new Product
                {
                    Name = "Мед, 500 мл",
                    Description = "Баночка меда с нашей пасеки",
                    Category = context.Categories.FirstOrDefault(x => x.Name == "Продукты пчеловодства"),
                    Price = 300M,
                },
                new Product
                {
                    Name = "Перга, 200 гр",
                    Description = "Перга",
                    Category = context.Categories.FirstOrDefault(x => x.Name == "Продукты пчеловодства"),
                    Price = 500M,
                },
                new Product
                {
                    Name = "Забрус, 300 гр",
                    Description = "Забрус",
                    Category = context.Categories.FirstOrDefault(x => x.Name == "Продукты пчеловодства"),
                    Price = 350M,
                },
                new Product
                {
                    Name = "Набор подарочный 'Новогодний'",
                    Description = "Подарочный набор",
                    Category = context.Categories.FirstOrDefault(x => x.Name == "Подарочные наборы"),
                    Price = 4500M,
                },
                new Product
                {
                    Name = "Набор подарочный 'Летний'",
                    Description = "Подарочный набор",
                    Category = context.Categories.FirstOrDefault(x => x.Name == "Подарочные наборы"),
                    Price = 4500M,
                }
                );

            }
            context.SaveChanges();
        }
    }
}

