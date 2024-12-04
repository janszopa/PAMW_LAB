using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using Shared.Services;
using Domain.Models;
using Shared;

namespace MyRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
        {
            var response = await _categoryService.GetCategoriesAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Category>>> GetCategory(int id)
        {
            var response = await _categoryService.GetCategoryAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Category>>> CreateCategory(Category category)
        {
            var response = await _categoryService.CreateCategoryAsync(category);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCategory([FromRoute] int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return NotFound(response.Message);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Category>>> UpdateCategory(Category category)
        {
            var response = await _categoryService.UpdateCategoryAsync(category);

            if (response.Success)
                return Ok(response);
            else
                return NotFound(response.Message);
        }
    }
}