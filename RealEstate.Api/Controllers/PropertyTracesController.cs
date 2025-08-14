using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;

namespace RealEstate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTracesController : ControllerBase
    {
        private readonly IPropertyTraceRepository _repository;
        private readonly IMapperService _mapper;

        public PropertyTracesController(IPropertyTraceRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET traces por propiedad
        [HttpGet("property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyTraceDto>>> GetByPropertyId(int propertyId)
        {
            var traces = await _repository.GetByPropertyIdAsync(propertyId);
            var data = traces.Select(t => _mapper.MapToDto(t));
            return Ok(data);
        }

        // GET detalle por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyTraceDto>> GetById(int id)
        {
            var trace = await _repository.GetByIdAsync(id);
            if (trace == null)
                return NotFound();

            return Ok(_mapper.MapToDto(trace));
        }

        // POST crear
        [HttpPost]
        public async Task<ActionResult<PropertyTraceDto>> Create([FromBody] PropertyTraceDto dto)
        {
            var trace = _mapper.MapToEntity(dto);
            var created = await _repository.CreateAsync(trace);
            var createdDto = _mapper.MapToDto(created);

            return CreatedAtAction(nameof(GetById), new { id = created.IdPropertyTrace }, createdDto);
        }

        // PUT actualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PropertyTraceDto dto)
        {
            var trace = _mapper.MapToEntity(dto);
            trace.IdPropertyTrace = id;

            var updated = await _repository.UpdateAsync(id, trace);
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
    }
}