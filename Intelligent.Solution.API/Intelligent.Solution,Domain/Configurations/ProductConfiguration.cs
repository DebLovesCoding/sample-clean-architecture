using Intelligent.Solution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intelligent.Solution.Domain.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product), "dbo");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Code).HasMaxLength(50);
            builder.Property(p=> p.Description).HasMaxLength(500);
        }
    }
}
