using Shared.Services;
using MyRestApi.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace MyRestApi.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly DataContext _dataContext;

        public ContactInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<ContactInfo>> CreateContactInfoAsync(ContactInfo newContactInfo)
        {
            var result = new ServiceResponse<ContactInfo>();

            try
            {
                await _dataContext.ContactInfos.AddAsync(newContactInfo);
                await _dataContext.SaveChangesAsync();

                result.Data = newContactInfo;
                result.Success = true;
                result.Message = "Data created successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while creating new contact info {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteContactInfoAsync(int id)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var contactInfo = await _dataContext.ContactInfos.FirstOrDefaultAsync(p => p.Id == id);

                if (contactInfo != null)
                {
                    _dataContext.ContactInfos.Remove(contactInfo);
                    await _dataContext.SaveChangesAsync();

                    result.Data = true;
                    result.Success = true;
                    result.Message = "Data deleted successfully";
                }
                else
                {
                    result.Message = "Contact info not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while deleting contact info {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<List<ContactInfo>>> GetContactInfosAsync()
        {
            var result = new ServiceResponse<List<ContactInfo>>();

            try
            {
                result.Data = await _dataContext.ContactInfos.ToListAsync();
                result.Success = true;
                result.Message = "Data retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while retrieving contact infos {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<ContactInfo>> GetContactInfoAsync(int id)
        {
            var result = new ServiceResponse<ContactInfo>();

            try
            {
                result.Data = await _dataContext.ContactInfos.FirstOrDefaultAsync(p => p.Id == id);
                result.Success = true;
                result.Message = "Data retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while retrieving contact info {ex.Message}";
                result.Success = false;
            }

            return result;
        }
    
        public async Task<ServiceResponse<ContactInfo>> UpdateContactInfoAsync(ContactInfo updatedContactInfo)
        {
            var result = new ServiceResponse<ContactInfo>();

            try
            {
                var contactInfo = await _dataContext.ContactInfos.FirstOrDefaultAsync(p => p.Id == updatedContactInfo.Id);

                if (contactInfo != null)
                {
                    contactInfo.PhoneNumber = updatedContactInfo.PhoneNumber;
                    contactInfo.Email = updatedContactInfo.Email;

                    _dataContext.ContactInfos.Update(contactInfo);
                    await _dataContext.SaveChangesAsync();

                    result.Data = contactInfo;
                    result.Success = true;
                    result.Message = "Data updated successfully";
                }
                else
                {
                    result.Message = "Contact info not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while updating contact info {ex.Message}";
                result.Success = false;
            }

            return result;
        }
    }
}