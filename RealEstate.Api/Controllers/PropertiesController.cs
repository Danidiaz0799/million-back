using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

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
        public async Task<ActionResult<IEnumerable<Property>>> Get(
            string? name,
            string? address,
            decimal? priceMin,
            decimal? priceMax,
            int page = 1,
            int pageSize = 10,
            string? sortField = null,
            bool sortDescending = false)
        {
            var result = await _repository.GetPropertiesAsync(
                name, address, priceMin, priceMax, page, pageSize, sortField, sortDescending);
            return Ok(result);
        }
    }
}
