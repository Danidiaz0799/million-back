using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;

namespace RealEstate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _repository;
        private readonly IMapperService _mapper;

        public OwnersController(IOwnerRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET todos los owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> Get()
        {
            var owners = await _repository.GetOwnersAsync();
            var data = owners.Select(o => _mapper.MapToDto(o));
            return Ok(data);
        }

        // GET detalle por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDto>> GetById(int id)
        {
            var owner = await _repository.GetByIdAsync(id);
            if (owner == null)
                return NotFound();

            return Ok(_mapper.MapToDto(owner));
        }

        // POST crear
        [HttpPost]
        public async Task<ActionResult<OwnerDto>> Create([FromBody] OwnerDto dto)
        {
            var owner = _mapper.MapToEntity(dto);
            var created = await _repository.CreateAsync(owner);
            var createdDto = _mapper.MapToDto(created);

            return CreatedAtAction(nameof(GetById), new { id = created.IdOwner }, createdDto);
        }

        // PUT actualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OwnerDto dto)
        {
            var owner = _mapper.MapToEntity(dto);
            owner.IdOwner = id;

            var updated = await _repository.UpdateAsync(id, owner);
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