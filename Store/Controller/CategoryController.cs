using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new BaseResponse { Data = await _categoryService.GetAllAsync(), Message = "Get Category Success" });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            return Ok(new BaseResponse
            {
                Data = _mapper.Map<CategoryDto>(await _categoryService.GetCategoryById(id)),
                Message = "Get Category Success"
            });
        }
        [Authorize(Roles = "Admin, Staff")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            await _categoryService.AddCategoryAsync(createCategoryDto);
            return Ok(new BaseResponse { Message = "Create Category Success" });

        }

        [Authorize(Roles = "Admin, Staff")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategory(id, updateCategoryDto);
            return Ok(new BaseResponse { Message = "Update Category Success" });
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok(new BaseResponse { Message = "Delete Category Success" });
        }

        [Authorize(Roles = "Admin, Staff")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRangeCategory([FromBody] List<CreateCategoryDto> createCategoryDtos)
        {
            await _categoryService.AddRangeCategory(createCategoryDtos);
            return Ok(new BaseResponse { Message = "Add Categories Success" });
        }

        [Authorize(Roles = "Admin, Staff")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteRangeCategory([FromBody] List<int> ids)
        {
            await _categoryService.DeleteRangeCategory(ids);
            return Ok(new BaseResponse { Message = "Delete Categories Success" });
        }
    }
}
