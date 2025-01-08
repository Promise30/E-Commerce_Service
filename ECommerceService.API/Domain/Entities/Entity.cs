namespace ECommerceService.API.Domain.Entities
{
    public abstract class Entity<Tkey>
    {
        public Tkey Id { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        protected Entity(Tkey id)
        {
            this.Id = id;
            this.CreatedDate = DateTimeOffset.Now;
            this.LastModifiedDate = DateTimeOffset.Now;
        }

        protected Entity()
        {

        }
    }
}
