namespace Store.Entity
{
    public class Customer : BaseEntity
    {
        
        public int Id { get; set; }
        public string CusName { get; set; }
        public string Address { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
