using Microsoft.AspNetCore.Mvc;
using MyRestApi.Models;
using Shared.Services;
using Domain.Models;
using Shared;

namespace MyRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoService _contactInfoService;

        public ContactInfoController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ContactInfo>>>> GetContactInfos()
        {
            var response = await _contactInfoService.GetContactInfosAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ContactInfo>>> GetContactInfo(int id)
        {
            var response = await _contactInfoService.GetContactInfoAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ContactInfo>>> CreateContactInfo(ContactInfo contactInfo)
        {
            var response = await _contactInfoService.CreateContactInfoAsync(contactInfo);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteContactInfo([FromRoute] int id)
        {
            var response = await _contactInfoService.DeleteContactInfoAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ContactInfo>>> UpdateContactInfo(ContactInfo updatedContactInfo)
        {
            var response = await _contactInfoService.UpdateContactInfoAsync(updatedContactInfo);

            if (response.Success)
                return Ok(response);
            else
                return NotFound(response);
        }
    }
}