using Pratica.Domain.Models.Base;

namespace Pratica.Domain.Models
{
    public class OrderItemModel : EntityBase
    {
        public Guid OrderId { get; set; }
        public OrderModel Order { get; set; }
        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; }
        public decimal SellValue { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmout { get; set; }
    }
}
