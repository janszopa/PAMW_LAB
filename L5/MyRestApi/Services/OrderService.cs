using Shared.Services;
using MyRestApi.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace MyRestApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;

        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Order>> CreateOrderAsync(Order newOrder)
        {
            var result = new ServiceResponse<Order>();

            try
            {
                await _dataContext.Orders.AddAsync(newOrder);
                await _dataContext.SaveChangesAsync();

                result.Data = newOrder;
                result.Success = true;
                result.Message = "Data created successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while creating new order {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteOrderAsync(int id)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var order = await _dataContext.Orders.FirstOrDefaultAsync(p => p.Id == id);

                if (order != null)
                {
                    _dataContext.Orders.Remove(order);
                    await _dataContext.SaveChangesAsync();

                    result.Data = true;
                    result.Success = true;
                    result.Message = "Data deleted successfully";
                }
                else
                {
                    result.Message = "Order not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while deleting order {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<Order>> GetOrderAsync(int id)
        {
            var result = new ServiceResponse<Order>();

            try
            {
                result.Data = await _dataContext.Orders.FirstOrDefaultAsync(p => p.Id == id);
                result.Success = true;
                result.Message = "Data retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while retrieving order {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<List<Order>>> GetOrdersAsync()
        {
            var result = new ServiceResponse<List<Order>>();

            try
            {
                result.Data = await _dataContext.Orders.ToListAsync();
                result.Success = true;
                result.Message = "Data retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while retrieving orders {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<Order>> UpdateOrderAsync(Order updatedOrder)
        {
            var result = new ServiceResponse<Order>();

            try
            {
                var order = await _dataContext.Orders.FirstOrDefaultAsync(p => p.Id == updatedOrder.Id);

                if (order != null)
                {
                    order.OrderDate = updatedOrder.OrderDate;
                    order.Products = updatedOrder.Products;

                    _dataContext.Orders.Update(order);
                    await _dataContext.SaveChangesAsync();

                    result.Data = order;
                    result.Success = true;
                    result.Message = "Data updated successfully";
                }
                else
                {
                    result.Message = "Order not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while updating order {ex.Message}";
                result.Success = false;
            }

            return result;
        }
    }
}