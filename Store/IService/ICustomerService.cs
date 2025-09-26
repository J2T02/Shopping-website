using Store.DTOs.Customer;
using Store.Entity;

namespace Store.IService
{
    public interface ICustomerService 
    {
        Task<List<CustomerDto>?> GetAllCustomer();
        Task AddCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<Customer> GetCustomerById(int id);
        Task<CustomerDto> GetCustomerByAccountId(int accountId);
        Task UpdateCustomer(int customerId ,UpdateCustomerDto updateCustomerDto);

    }
}
