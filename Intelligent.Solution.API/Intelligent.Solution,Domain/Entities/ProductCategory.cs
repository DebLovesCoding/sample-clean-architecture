namespace Intelligent.Solution.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public virtual List<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = [];
        }
    }
}
