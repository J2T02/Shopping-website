using System.ComponentModel.DataAnnotations;

namespace Store.DTOs.Customer
{
    public class CreateCustomerDto
    {

        public string CusName { get; set; }
        public string Address { get; set; }
        public int AccountId { get; set; }
    }
}
