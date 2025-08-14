using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(
            string? name,
            string? address,
            decimal? priceMin,
            decimal? priceMax,
            int page,
            int pageSize,
            string? sortField,
            bool sortDescending);
    }
}
