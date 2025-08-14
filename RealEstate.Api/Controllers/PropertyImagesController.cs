using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Api.Controllers
{
    /// <summary>
    /// Controller para gestión de imágenes de propiedades
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PropertyImagesController : ControllerBase
    {
        private readonly IPropertyImageRepository _repository;
        private readonly IMapperService _mapper;

        public PropertyImagesController(IPropertyImageRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las imágenes de propiedades
        /// </summary>
        /// <returns>Lista de imágenes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyImageDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PropertyImageDto>>> Get()
        {
            // For now, return empty list - in a real scenario you might want pagination
            return Ok(new List<PropertyImageDto>());
        }

        /// <summary>
        /// Obtiene imágenes por ID de propiedad
        /// </summary>
        /// <param name="propertyId">ID de la propiedad</param>
        /// <returns>Lista de imágenes de la propiedad</returns>
        [HttpGet("property/{propertyId}")]
        [ProducesResponseType(typeof(IEnumerable<PropertyImageDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetByPropertyId([Required] int propertyId)
        {
            var images = await _repository.GetByPropertyIdAsync(propertyId);
            var data = images.Select(i => _mapper.MapToDto(i));
            return Ok(data);
        }

        /// <summary>
        /// Obtiene una imagen por ID
        /// </summary>
        /// <param name="id">ID de la imagen</param>
        /// <returns>Datos de la imagen</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PropertyImageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PropertyImageDto>> GetById([Required] int id)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            return Ok(_mapper.MapToDto(image));
        }

        /// <summary>
        /// Crea una nueva imagen de propiedad
        /// </summary>
        /// <param name="dto">Datos de la imagen</param>
        /// <returns>Imagen creada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PropertyImageDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PropertyImageDto>> Create([FromBody] PropertyImageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var image = _mapper.MapToEntity(dto);
            var created = await _repository.CreateAsync(image);
            var createdDto = _mapper.MapToDto(created);

            return CreatedAtAction(nameof(GetById), new { id = created.IdPropertyImage }, createdDto);
        }

        /// <summary>
        /// Actualiza una imagen existente
        /// </summary>
        /// <param name="id">ID de la imagen</param>
        /// <param name="dto">Nuevos datos de la imagen</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([Required] int id, [FromBody] PropertyImageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var image = _mapper.MapToEntity(dto);
            image.IdPropertyImage = id;

            var updated = await _repository.UpdateAsync(id, image);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Elimina una imagen
        /// </summary>
        /// <param name="id">ID de la imagen</param>
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