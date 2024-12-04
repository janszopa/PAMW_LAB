using Shared.Services;
using MyRestApi.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace MyRestApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Category>> CreateCategoryAsync(Category newCategory)
        {
            var result = new ServiceResponse<Category>();

            try
            {
                await _dataContext.Categories.AddAsync(newCategory);
                await _dataContext.SaveChangesAsync();

                result.Data = newCategory;
                result.Success = true;
                result.Message = "Data created successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while creating new category {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var category = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Id == id);

                if (category != null)
                {
                    _dataContext.Categories.Remove(category);
                    await _dataContext.SaveChangesAsync();

                    result.Data = true;
                    result.Success = true;
                    result.Message = "Data deleted successfully";
                }
                else
                {
                    result.Message = "Category not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while deleting category {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<Category>> GetCategoryAsync(int id)
        {
            var result = new ServiceResponse<Category>();

            try
            {
                var category = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Id == id);

                if (category != null)
                {
                    result.Data = category;
                    result.Success = true;
                    result.Message = "Data found successfully";
                }
                else
                {
                    result.Message = "Category not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while getting category {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var result = new ServiceResponse<List<Category>>();

            try
            {
                result.Data = await _dataContext.Categories.ToListAsync();
                result.Success = true;
                result.Message = "Data found successfully";
            } 
            catch (Exception ex)
            {
                result.Message = $"Error while getting categories {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<Category>> UpdateCategoryAsync(Category updatedCategory)
        {
            var result = new ServiceResponse<Category>();

            try
            {
                var category = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Id == updatedCategory.Id);

                if (category != null)
                {
                    category.Name = updatedCategory.Name;
                    await _dataContext.SaveChangesAsync();

                    result.Data = category;
                    result.Success = true;
                    result.Message = "Data updated successfully";
                }
                else
                {
                    result.Message = "Category not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while updating category {ex.Message}";
                result.Success = false;
            }

            return result;
        }
    }    
}