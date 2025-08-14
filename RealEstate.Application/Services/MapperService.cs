using RealEstate.Domain.Entities;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;

namespace RealEstate.Application.Services
{
    public class MapperService : IMapperService
    {
        // Property mappings
        public PropertyDto MapToDto(Property p) =>
            new PropertyDto
            {
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                CodeInternal = p.CodeInternal,
                Year = p.Year,
                IdOwner = p.IdOwner
            };

        public Property MapToEntity(PropertyDto dto) =>
            new Property
            {
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                CodeInternal = dto.CodeInternal,
                Year = dto.Year,
                IdOwner = dto.IdOwner
            };

        // Owner mappings
        public OwnerDto MapToDto(Owner o) =>
            new OwnerDto
            {
                Name = o.Name,
                Address = o.Address,
                Photo = o.Photo,
                Birthday = o.Birthday
            };

        public Owner MapToEntity(OwnerDto dto) =>
            new Owner
            {
                Name = dto.Name,
                Address = dto.Address,
                Photo = dto.Photo,
                Birthday = dto.Birthday
            };

        // PropertyImage mappings
        public PropertyImageDto MapToDto(PropertyImage pi) =>
            new PropertyImageDto
            {
                IdProperty = pi.IdProperty,
                File = pi.File,
                Enabled = pi.Enabled
            };

        public PropertyImage MapToEntity(PropertyImageDto dto) =>
            new PropertyImage
            {
                IdProperty = dto.IdProperty,
                File = dto.File,
                Enabled = dto.Enabled
            };

        // PropertyTrace mappings
        public PropertyTraceDto MapToDto(PropertyTrace pt) =>
            new PropertyTraceDto
            {
                DateSale = pt.DateSale,
                Name = pt.Name,
                Value = pt.Value,
                Tax = pt.Tax,
                IdProperty = pt.IdProperty
            };

        public PropertyTrace MapToEntity(PropertyTraceDto dto) =>
            new PropertyTrace
            {
                DateSale = dto.DateSale,
                Name = dto.Name,
                Value = dto.Value,
                Tax = dto.Tax,
                IdProperty = dto.IdProperty
            };
    }
}
