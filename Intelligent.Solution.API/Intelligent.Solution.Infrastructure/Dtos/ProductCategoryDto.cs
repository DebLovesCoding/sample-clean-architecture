namespace Intelligent.Solution.Infrastructure.Dtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<ProductDto> Products { get; set; }

        public ProductCategoryDto()
        {
            Products = [];
        }
    }
}
