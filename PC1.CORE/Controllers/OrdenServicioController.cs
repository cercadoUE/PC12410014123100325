using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PC1.CORE.Core.DTOs;
using PC1.CORE.Core.Interfaces;

namespace PC1.CORE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdenServicioController : ControllerBase
    {
        private readonly IOrdenServicioRepository _repository;

        public OrdenServicioController(IOrdenServicioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenServicioReadDto>>> Get()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenServicioReadDto>> Get(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<OrdenServicioReadDto>> Post([FromBody] OrdenServicioCreateDto dto)
        {
            var created = await _repository.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrdenServicioCreateDto dto)
        {
            var updated = await _repository.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
