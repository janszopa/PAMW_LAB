using Shared;
using Domain.Models;

namespace Shared.Services
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<Order>>> GetOrdersAsync();
        Task<ServiceResponse<Order>> CreateOrderAsync(Order newOrder);
        Task<ServiceResponse<bool>> DeleteOrderAsync(int id);
        Task<ServiceResponse<Order>> UpdateOrderAsync(Order updatedOrder);
        Task<ServiceResponse<Order>> GetOrderAsync(int id);
    }
}