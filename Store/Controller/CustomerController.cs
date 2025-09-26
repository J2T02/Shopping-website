using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.DTOs.Common;
using Store.DTOs.Customer;
using Store.IService;

namespace Store.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCustomer()
        {
            return Ok(new BaseResponse { Data= await _customerService.GetAllCustomer(), Message = "Get customers success" });
        }

        [HttpGet("[action]/{accountId}")]
        public async Task<IActionResult> GetCustomerByAccountId([FromRoute] int accountId)
        {
            var customer = await _customerService.GetCustomerByAccountId(accountId);
            if(customer == null)
            {
                return NotFound(new BaseResponse { Message = "Customer not found!" });
            }
            return Ok(new BaseResponse { Data = customer, Message = "Get customer success" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            await _customerService.AddCustomerAsync(createCustomerDto);
            return Ok(new BaseResponse { Message = "Add Customer success" });
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id,[FromBody] UpdateCustomerDto updateCustomerDto)
        {
            await _customerService.UpdateCustomer(id, updateCustomerDto);
            return Ok(new BaseResponse { Message = "Update customer success" });
        }
    }
}
