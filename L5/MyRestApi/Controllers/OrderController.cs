using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using Shared.Services;
using Domain.Models;
using Shared;

namespace MyRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Order>>> CreateOrder(Order order)
        {
            var response = await _orderService.CreateOrderAsync(order);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteOrder([FromRoute] int id)
        {
            var response = await _orderService.DeleteOrderAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Order>>>> GetOrders()
        {
            var response = await _orderService.GetOrdersAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Order>>> GetOrder(int id)
        {
            var response = await _orderService.GetOrderAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Order>>> UpdateOrder(Order order)
        {
            var response = await _orderService.UpdateOrderAsync(order);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
    }
}