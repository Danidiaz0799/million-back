using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Interfaces;
using RealEstate.Api.DTOs;

namespace RealEstate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _repository;

        public PropertiesController(IPropertyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> Get(
            string? name,
            string? address,
            decimal? priceMin,
            decimal? priceMax,
            int page = 1,
            int pageSize = 10,
            string? sortField = null,
            bool sortDescending = false)
        {
            var properties = await _repository.GetPropertiesAsync(
                name, address, priceMin, priceMax, page, pageSize, sortField, sortDescending);

            var result = properties.Select(p => new PropertyDto
            {
                IdOwner = p.OwnerId,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            });

            return Ok(result);
        }
    }
}
