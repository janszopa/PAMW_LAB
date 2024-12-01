using Shared;
using Domain.Models;

namespace Shared.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> CreateProductAsync(Product newProduct);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
        Task<ServiceResponse<Product>> UpdateProductAsync(Product updatedProduct);
        Task<ServiceResponse<Product>> GetProductAsync(int id);
     
    }
}