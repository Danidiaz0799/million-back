using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IPropertyTraceRepository
    {
        Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(int propertyId);
        Task<PropertyTrace?> GetByIdAsync(int id);
        Task<PropertyTrace> CreateAsync(PropertyTrace propertyTrace);
        Task<bool> UpdateAsync(int id, PropertyTrace propertyTrace);
        Task<bool> DeleteAsync(int id);
    }
}