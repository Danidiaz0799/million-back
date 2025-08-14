using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Api.Controllers
{
    /// <summary>
    /// Controller para gestión de propietarios
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _repository;
        private readonly IMapperService _mapper;

        public OwnersController(IOwnerRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los propietarios
        /// </summary>
        /// <returns>Lista de propietarios</returns>
        /// <response code="200">Retorna la lista de propietarios</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OwnerDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> Get()
        {
            var owners = await _repository.GetOwnersAsync();
            var data = owners.Select(o => _mapper.MapToDto(o));
            return Ok(data);
        }

        /// <summary>
        /// Obtiene un propietario por ID
        /// </summary>
        /// <param name="id">ID del propietario</param>
        /// <returns>Datos del propietario</returns>
        /// <response code="200">Retorna el propietario encontrado</response>
        /// <response code="404">Propietario no encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OwnerDto>> GetById([Required] int id)
        {
            var owner = await _repository.GetByIdAsync(id);
            if (owner == null)
                return NotFound();

            return Ok(_mapper.MapToDto(owner));
        }

        /// <summary>
        /// Crea un nuevo propietario
        /// </summary>
        /// <param name="dto">Datos del propietario a crear</param>
        /// <returns>Propietario creado</returns>
        /// <response code="201">Propietario creado exitosamente</response>
        /// <response code="400">Datos de entrada inválidos</response>
        [HttpPost]
        [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OwnerDto>> Create([FromBody] OwnerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = _mapper.MapToEntity(dto);
            var created = await _repository.CreateAsync(owner);
            var createdDto = _mapper.MapToDto(created);

            return CreatedAtAction(nameof(GetById), new { id = created.IdOwner }, createdDto);
        }

        /// <summary>
        /// Actualiza un propietario existente
        /// </summary>
        /// <param name="id">ID del propietario a actualizar</param>
        /// <param name="dto">Nuevos datos del propietario</param>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Propietario actualizado exitosamente</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="404">Propietario no encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([Required] int id, [FromBody] OwnerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = _mapper.MapToEntity(dto);
            owner.IdOwner = id;

            var updated = await _repository.UpdateAsync(id, owner);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Elimina un propietario
        /// </summary>
        /// <param name="id">ID del propietario a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Propietario eliminado exitosamente</response>
        /// <response code="404">Propietario no encontrado</response>
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