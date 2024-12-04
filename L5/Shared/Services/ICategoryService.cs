using Shared;
using Domain.Models;

namespace Shared.Services
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
        Task<ServiceResponse<Category>> CreateCategoryAsync(Category newCategory);
        Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
        Task<ServiceResponse<Category>> UpdateCategoryAsync(Category updatedCategory);
        Task<ServiceResponse<Category>> GetCategoryAsync(int id);
     
    }
}