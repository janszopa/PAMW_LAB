using Shared;
using Domain.Models;

namespace Shared.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetUsersAsync();
        Task<ServiceResponse<User>> CreateUserAsync(User newUser);
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
        Task<ServiceResponse<User>> UpdateUserAsync(User updatedUser);
        Task<ServiceResponse<User>> GetUserAsync(int id);
    }
}