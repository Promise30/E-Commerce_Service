using ECommerceService.API.Domain.Enums;
using System.ComponentModel;
using System.Reflection;

namespace ECommerceService.API.Domain.Entities
{
    public class Order : Entity<int>
    {
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public decimal TotalAmount { get; set; }
        public decimal DiscountApplied { get; set; } 
        public decimal TaxAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string UserId { get; set; }
        public string PaymentReference { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<OrderItem> OrderItems {  get; set; }
        public Order() { }
        public Order(int id) { }


    }
}
