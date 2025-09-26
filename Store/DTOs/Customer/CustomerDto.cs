using Store.Entity;

namespace Store.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CusName { get; set; }
        public string Address { get; set; }
        public int AccountId { get; set; }
    }
}
