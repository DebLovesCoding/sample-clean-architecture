using Intelligent.Solution.Domain.Configurations;
using Intelligent.Solution.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intelligent.Solution.Domain
{
    public class IntelligentContext: DbContext
    {
        public IntelligentContext(DbContextOptions<IntelligentContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
