using Microsoft.EntityFrameworkCore;
using Shared.Services;
using Shared;
using Domain.Models;
using MyRestApi.Models;

namespace MyRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<User>> CreateUserAsync(User newUser)
        {
            var result = new ServiceResponse<User>();

            try
            {
                await _dataContext.Users.AddAsync(newUser);
                await _dataContext.SaveChangesAsync();

                result.Data = newUser;
                result.Success = true;
                result.Message = "Data created successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while creating new user {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(p => p.Id == id);

                if (user != null)
                {
                    _dataContext.Users.Remove(user);
                    await _dataContext.SaveChangesAsync();

                    result.Data = true;
                    result.Success = true;
                    result.Message = "Data deleted successfully";
                }
                else
                {
                    result.Message = "User not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while deleting user {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<User>> GetUserAsync(int id)
        {
            var result = new ServiceResponse<User>();

            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(p => p.Id == id);

                if (user != null)
                {
                    result.Data = user;
                    result.Success = true;
                    result.Message = "Data found successfully";
                }
                else
                {
                    result.Message = "User not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while getting user {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<List<User>>> GetUsersAsync()
        {
            var result = new ServiceResponse<List<User>>();

            try
            {
                result.Data = await _dataContext.Users.ToListAsync();
                result.Success = true;
                result.Message = "Data found successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while getting users {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<User>> UpdateUserAsync(User updatedUser)
        {
            var result = new ServiceResponse<User>();

            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(p => p.Id == updatedUser.Id);

                if (user != null)
                {
                    user.Username = updatedUser.Username;
                    user.ContactInfo = updatedUser.ContactInfo;

                    _dataContext.Users.Update(user);
                    await _dataContext.SaveChangesAsync();

                    result.Data = user;
                    result.Success = true;
                    result.Message = "Data updated successfully";
                }
                else
                {
                    result.Message = "User not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while updating user {ex.Message}";
                result.Success = false;
            }

            return result;
        }
    }
}