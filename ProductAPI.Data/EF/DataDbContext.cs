using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.Configurations;
using ProductAPI.Data.Entities;
using ProductAPI.Data.Extensions;

namespace ProductAPI.Data.EF
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions options) : base(options)
        {
        }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //Configure using Fluent API
                modelBuilder.ApplyConfiguration(new CategoryConfiguration());
                modelBuilder.ApplyConfiguration(new ProductConfiguration());
                modelBuilder.ApplyConfiguration(new CommentConfiguration());


            //Data seeding
            modelBuilder.Seed();
                //base.OnModelCreating(modelBuilder);
            }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
    
}
