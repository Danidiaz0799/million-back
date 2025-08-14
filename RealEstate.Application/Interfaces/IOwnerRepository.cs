using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetOwnersAsync();
        Task<Owner?> GetByIdAsync(int id);
        Task<Owner> CreateAsync(Owner owner);
        Task<bool> UpdateAsync(int id, Owner owner);
        Task<bool> DeleteAsync(int id);
    }
}