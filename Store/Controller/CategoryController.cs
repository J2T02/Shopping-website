using Microsoft.AspNetCore.Mvc;
using Store.DTOs.Category;
using Store.DTOs.Common;
using Store.IService;

namespace Store.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new BaseResponse { Data = await _categoryService.GetAllAsync(), Message = "Get Category Success" });
            }          
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse { Message = ex.Message });
            }
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            try
            {
                return Ok(new BaseResponse {Data = await _categoryService.GetByIdAsync(id), Message = "Get Category Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse { Message = ex.Message });
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {

                await _categoryService.AddCategoryAsync(createCategoryDto);
                return Ok(new BaseResponse {Message = "Create Category Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse { Message = ex.Message });
            }
        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            try
            {

                await _categoryService.UpdateCategory(id, updateCategoryDto);
                return Ok(new BaseResponse { Message = "Update Category Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse { Message = ex.Message });
            }
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);
                return Ok(new BaseResponse { Message = "Delete Category Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse { Message = ex.Message });
            }
        }
        
    }
}
