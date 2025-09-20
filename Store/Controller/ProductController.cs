using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.DTOs.Category;
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
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new BaseResponse { Data = await _productService.GetAllAsync(), Message = "Get Products Success" });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            return Ok(new BaseResponse
            {
                Data = _mapper.Map<CategoryDto>(await _productService.GetProductById(id)),
                Message = "Get Product Success"
            });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            await _productService.AddProductAsync(createProductDto);
            return Ok(new BaseResponse { Message = "Create Product Success" });

        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProduct(id, updateProductDto);
            return Ok(new BaseResponse { Message = "Update Product Success" });
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productService.DeleteProduct(id);
            return Ok(new BaseResponse { Message = "Delete Product Success" });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRangeProduct([FromBody] List<CreateProductDto> createProductDtos)
        {
            await _productService.AddRangeProduct(createProductDtos);
            return Ok(new BaseResponse { Message = "Add Products Success" });
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteRangeProduct([FromBody] List<int> ids)
        {
            await _productService.DeleteRangeProduct(ids);
            return Ok(new BaseResponse { Message = "Delete Products Success" });
        }
    }
}
