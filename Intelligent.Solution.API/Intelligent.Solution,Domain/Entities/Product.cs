namespace Intelligent.Solution.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
