using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using Shared.Services;
using Domain.Models;
using Shared;


namespace MyRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase 
    {
        private readonly IProductService _productService; //Dependency Injection

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var response = await _productService.GetProductsAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProduct(int id)
        {
            var response = await _productService.GetProductAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> CreateProduct(Product product)
        {
            var response = await _productService.CreateProductAsync(product);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct([FromRoute] int id)
        {
            var response = await _productService.DeleteProductAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return StatusCode(500, $"Internal server error {response.Message}");
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct([FromBody] Product updatedProduct)
        {
            var result = await _productService.UpdateProductAsync(updatedProduct);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }
    }
}
