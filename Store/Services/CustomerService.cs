using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.DTOs.Customer;
using Store.Entity;
using Store.IService;
using Store.Migrations;
using Store.Exceptions;
using Store.Repositories;

namespace Store.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public CustomerService(IRepository<Customer> customerRepository, IMapper mapper, IAccountService accountService)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task AddCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            if (string.IsNullOrEmpty(createCustomerDto.CusName))
            {
                throw new ValidationException("Customer name is required!");
            }
            if (string.IsNullOrEmpty(createCustomerDto.Address))
            {
                throw new ValidationException("Address is required!");
            }
            var customer = new Customer
            {
                AccountId = createCustomerDto.AccountId,
                Address = createCustomerDto.Address,
                CusName = createCustomerDto.CusName,
            };
            
            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangeAsync();
        }

        public async Task<List<CustomerDto>?> GetAllCustomer()
        {
            var customers = await _customerRepository.Get()
                .Include(x => x.Account).ThenInclude(x => x.Role)
                .ToListAsync();
            var customersDto = customers.Select(x => _mapper.Map<CustomerDto>(x)).ToList();
            return customersDto;
        }

        public async Task<CustomerDto> GetCustomerByAccountId(int accountId)
        {
            var customer = await _customerRepository.Get()
                .Include(x => x.Account).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.AccountId == accountId);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _customerRepository.Get()
                .Include(x => x.Account).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null) throw new NotFoundException("Customer not found!");
            return customer;
        }

        public async Task UpdateCustomer(int customerId, UpdateCustomerDto updateCustomerDto)
        {
            var currentCustomer = await GetCustomerById(customerId);
            if (string.IsNullOrEmpty(updateCustomerDto.Address) || string.IsNullOrEmpty(updateCustomerDto.Name))
            {
                throw new ValidationException("All fields is required!");
            }
            currentCustomer.Address = updateCustomerDto.Address;
            currentCustomer.CusName = updateCustomerDto.Name;
            _customerRepository.Update(currentCustomer);
            await _customerRepository.SaveChangeAsync();
        }


    }
}

