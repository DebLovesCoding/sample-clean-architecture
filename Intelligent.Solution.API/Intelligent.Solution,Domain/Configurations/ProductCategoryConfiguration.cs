using Intelligent.Solution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intelligent.Solution.Domain.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable(nameof(ProductCategory), "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.HasMany(x => x.Products).WithOne(x => x.ProductCategory).HasForeignKey(x => x.ProductCategoryId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
