namespace ECommerceService.API.Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int StockQuantity  { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Product(Guid id) : base(id)
        {
        }
        public Product()
        {

        }
    }
}
