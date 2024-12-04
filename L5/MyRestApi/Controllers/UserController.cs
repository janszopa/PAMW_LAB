using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using Shared.Services;
using Domain.Models;
using Shared;


namespace MyRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase 
    {
        private readonly IUserService _userService; //Dependency Injection

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers()
        {
            var response = await _userService.GetUsersAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUser(int id)
        {
            var response = await _userService.GetUserAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<User>>>> CreateUser(User user)
        {
            var response = await _userService.CreateUserAsync(user);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser([FromRoute] int id)
        {
            var response = await _userService.DeleteUserAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return StatusCode(500, $"Internal server error {response.Message}");
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<User>>> UpdateUser(User user)
        {
            var response = await _userService.UpdateUserAsync(user);

            if (response.Success)
                return Ok(response);
            else
                return StatusCode(500, $"Internal server error {response.Message}");
        }
    }
}