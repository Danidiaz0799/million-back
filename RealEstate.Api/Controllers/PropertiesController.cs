using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapperService _mapper;

        public PropertiesController(IPropertyRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET listado con filtros
        [HttpGet]
        public async Task<ActionResult<PagedResult<PropertyDto>>> Get(
            string? name,
            string? address,
            decimal? priceMin,
            decimal? priceMax,
            int page = 1,
            int pageSize = 10,
            string? sortField = null,
            bool sortDescending = false)
        {
            if (page < 1) return BadRequest("page must be >= 1");
            if (pageSize < 1 || pageSize > 100) return BadRequest("pageSize must be between 1 and 100");

            var (items, total) = await _repository.GetPropertiesAsync(
                name, address, priceMin, priceMax, page, pageSize, sortField, sortDescending);

            var data = items.Select(p => _mapper.MapToDto(p));

            var result = new PagedResult<PropertyDto>
            {
                Data = data,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };

            return Ok(result);
        }

        // GET detalle por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> GetById(int id)
        {
            var property = await _repository.GetByIdAsync(id);
            if (property == null)
                return NotFound();

            return Ok(_mapper.MapToDto(property));
        }

        // POST crear
        [HttpPost]
        public async Task<ActionResult<PropertyDto>> Create([FromBody] PropertyDto dto)
        {
            var property = _mapper.MapToEntity(dto);
            var created = await _repository.CreateAsync(property);
            var createdDto = _mapper.MapToDto(created);

            return CreatedAtAction(nameof(GetById), new { id = created.IdProperty }, createdDto);
        }

        // PUT actualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PropertyDto dto)
        {
            var property = _mapper.MapToEntity(dto);
            property.IdProperty = id;

            var updated = await _repository.UpdateAsync(id, property);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE eliminar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // GET propiedades por owner
        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetByOwnerId(int ownerId)
        {
            var properties = await _repository.GetByOwnerIdAsync(ownerId);
            var data = properties.Select(p => _mapper.MapToDto(p));
            return Ok(data);
        }
    }
}
