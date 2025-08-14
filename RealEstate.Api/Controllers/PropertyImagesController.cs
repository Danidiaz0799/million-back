using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;

namespace RealEstate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyImagesController : ControllerBase
    {
        private readonly IPropertyImageRepository _repository;
        private readonly IMapperService _mapper;

        public PropertyImagesController(IPropertyImageRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET imágenes por propiedad
        [HttpGet("property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetByPropertyId(int propertyId)
        {
            var images = await _repository.GetByPropertyIdAsync(propertyId);
            var data = images.Select(i => _mapper.MapToDto(i));
            return Ok(data);
        }

        // GET detalle por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyImageDto>> GetById(int id)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            return Ok(_mapper.MapToDto(image));
        }

        // POST crear
        [HttpPost]
        public async Task<ActionResult<PropertyImageDto>> Create([FromBody] PropertyImageDto dto)
        {
            var image = _mapper.MapToEntity(dto);
            var created = await _repository.CreateAsync(image);
            var createdDto = _mapper.MapToDto(created);

            return CreatedAtAction(nameof(GetById), new { id = created.IdPropertyImage }, createdDto);
        }

        // PUT actualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PropertyImageDto dto)
        {
            var image = _mapper.MapToEntity(dto);
            image.IdPropertyImage = id;

            var updated = await _repository.UpdateAsync(id, image);
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