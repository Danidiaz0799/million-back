using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        public Task<IEnumerable<Property>> GetPropertiesAsync(
            string? name,
            string? address,
            decimal? priceMin,
            decimal? priceMax,
            int page,
            int pageSize,
            string? sortField,
            bool sortDescending)
        {
            // Implementación real vendrá después (MongoDB)
            return Task.FromResult<IEnumerable<Property>>(new List<Property>());
        }
    }
}
