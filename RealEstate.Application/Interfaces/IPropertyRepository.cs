using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IPropertyRepository
    {
        Task<(IEnumerable<Property> Items, long Total)> GetPropertiesAsync(
            string? name,
            string? address,
            decimal? priceMin,
            decimal? priceMax,
            int page,
            int pageSize,
            string? sortField,
            bool sortDescending);

        Task<Property?> GetByIdAsync(int id);
        Task<Property> CreateAsync(Property property);
        Task<bool> UpdateAsync(int id, Property property);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Property>> GetByOwnerIdAsync(int ownerId);
    }
}
