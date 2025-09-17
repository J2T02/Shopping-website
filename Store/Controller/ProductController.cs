using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.DTOs.Common;
using Store.DTOs.Product;
using Store.IService;
using Store.Services;

namespace Store.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new BaseResponse { Data = await _productService.GetAllAsync(), Message = "Get Product Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse { Message = ex.Message });
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                await _productService.AddProductAsync(createProductDto);
                return Ok(new BaseResponse { Message = "Add Product Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse { Message = ex.Message });
            }
        }
    }
}
