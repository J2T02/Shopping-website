using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Entity
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }       
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public Transaction Transaction { get; set; }
        public int TransactionId { get; set; }
        public string ShippingAddress { get; set; }

    }
    public enum OrderStatus
    {
        Pending, Paid
    }
}
