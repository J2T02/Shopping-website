namespace Store.Entity
{
    public class Wallet : BaseEntity
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
