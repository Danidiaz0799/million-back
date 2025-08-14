using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(int propertyId);
        Task<PropertyImage?> GetByIdAsync(int id);
        Task<PropertyImage> CreateAsync(PropertyImage propertyImage);
        Task<bool> UpdateAsync(int id, PropertyImage propertyImage);
        Task<bool> DeleteAsync(int id);
    }
}