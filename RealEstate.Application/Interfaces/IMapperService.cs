using RealEstate.Domain.Entities;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Interfaces
{
    public interface IMapperService
    {
        PropertyDto MapToDto(Property p);
        Property MapToEntity(PropertyDto dto);
    }
}
