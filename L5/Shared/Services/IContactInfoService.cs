using Shared;
using Domain.Models;

namespace Shared.Services
{
    public interface IContactInfoService
    {
        Task<ServiceResponse<List<ContactInfo>>> GetContactInfosAsync();
        Task<ServiceResponse<ContactInfo>> CreateContactInfoAsync(ContactInfo newContactInfo);
        Task<ServiceResponse<bool>> DeleteContactInfoAsync(int id);
        Task<ServiceResponse<ContactInfo>> UpdateContactInfoAsync(ContactInfo updatedContactInfo);
        Task<ServiceResponse<ContactInfo>> GetContactInfoAsync(int id);
    }
}