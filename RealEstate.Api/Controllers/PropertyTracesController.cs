using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Api.Controllers
{
    /// <summary>
    /// Controller para gestión de trazas de propiedades
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PropertyTracesController : ControllerBase
    {
        private readonly IPropertyTraceRepository _repository;
        private readonly IMapperService _mapper;

        public PropertyTracesController(IPropertyTraceRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las trazas de propiedades
        /// </summary>
        /// <returns>Lista de trazas</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyTraceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PropertyTraceDto>>> Get()
        {
            // For now, return empty list - in a real scenario you might want pagination
            return Ok(new List<PropertyTraceDto>());
        }

        /// <summary>
        /// Obtiene trazas por ID de propiedad
        /// </summary>
        /// <param name="propertyId">ID de la propiedad</param>
        /// <returns>Lista de trazas de la propiedad</returns>
        [HttpGet("property/{propertyId}")]
        [ProducesResponseType(typeof(IEnumerable<PropertyTraceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PropertyTraceDto>>> GetByPropertyId([Required] int propertyId)
        {
            var traces = await _repository.GetByPropertyIdAsync(propertyId);
            var data = traces.Select(t => _mapper.MapToDto(t));
            return Ok(data);
        }

        /// <summary>
        /// Obtiene una traza por ID
        /// </summary>
        /// <param name="id">ID de la traza</param>
        /// <returns>Datos de la traza</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PropertyTraceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PropertyTraceDto>> GetById([Required] int id)
        {
            var trace = await _repository.GetByIdAsync(id);
            if (trace == null)
                return NotFound();

            return Ok(_mapper.MapToDto(trace));
        }

        /// <summary>
        /// Crea una nueva traza de propiedad
        /// </summary>
        /// <param name="dto">Datos de la traza</param>
        /// <returns>Traza creada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PropertyTraceDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PropertyTraceDto>> Create([FromBody] PropertyTraceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trace = _mapper.MapToEntity(dto);
            var created = await _repository.CreateAsync(trace);
            var createdDto = _mapper.MapToDto(created);

            return CreatedAtAction(nameof(GetById), new { id = created.IdPropertyTrace }, createdDto);
        }

        /// <summary>
        /// Actualiza una traza existente
        /// </summary>
        /// <param name="id">ID de la traza</param>
        /// <param name="dto">Nuevos datos de la traza</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([Required] int id, [FromBody] PropertyTraceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trace = _mapper.MapToEntity(dto);
            trace.IdPropertyTrace = id;

            var updated = await _repository.UpdateAsync(id, trace);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Elimina una traza
        /// </summary>
        /// <param name="id">ID de la traza</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([Required] int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}