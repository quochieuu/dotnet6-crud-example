using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var shoeCategoryId = new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97b");
            var clothingCategoryId = new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97c");
            var productId = new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97f");

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = shoeCategoryId,
                    Name = "Shoes",
                    ParentId = null,
                    Status = true,
                },
                 new Category()
                 {
                     Id = clothingCategoryId,
                     Name = "Clothing",
                     ParentId = null,
                     Status = true,
                 });

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = productId,
                    Name = "Nike",
                    Content = "New fashion 2021",
                    Price = 120000,
                    Status = true,
                    CreatedDate = DateTime.Now,
                    UrlImage = null,
                    CategoryId = shoeCategoryId
                }
                );
        }
    }
}
