namespace ECommerceService.API.Domain.Entities
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public Category(int id) : base(id)
        {
        }
        public Category()
        {
        }
    }
}
