using RealEstate.Domain.Entities;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;

namespace RealEstate.Application.Services
{
    public class MapperService : IMapperService
    {
        public PropertyDto MapToDto(Property p) =>
            new PropertyDto
            {
                IdOwner = p.OwnerId,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            };

        public Property MapToEntity(PropertyDto dto) =>
            new Property
            {
                OwnerId = dto.IdOwner,
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl
            };
    }
}
