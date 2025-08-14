using RealEstate.Domain.Entities;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Interfaces
{
    public interface IMapperService
    {
        // Property mappings
        PropertyDto MapToDto(Property p);
        Property MapToEntity(PropertyDto dto);
        
        // Owner mappings
        OwnerDto MapToDto(Owner o);
        Owner MapToEntity(OwnerDto dto);
        
        // PropertyImage mappings
        PropertyImageDto MapToDto(PropertyImage pi);
        PropertyImage MapToEntity(PropertyImageDto dto);
        
        // PropertyTrace mappings
        PropertyTraceDto MapToDto(PropertyTrace pt);
        PropertyTrace MapToEntity(PropertyTraceDto dto);
    }
}
