using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC1.CORE.Core.Entities;

namespace PC1.CORE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoServicioController : ControllerBase
    {
        private readonly TallerMecanicoDbContext _context;

        public TipoServicioController(TallerMecanicoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServicio>>> Get()
        {
            var tipos = await _context.TipoServicio.AsNoTracking().ToListAsync();
            return Ok(tipos);
        }

        [HttpGet("{id}", Name = "GetTipoServicio")]
        public async Task<ActionResult<TipoServicio>> Get(int id)
        {
            var tipo = await _context.TipoServicio.FindAsync(id);
            if (tipo == null) return NotFound();
            return Ok(tipo);
        }

        [HttpPost]
        public async Task<ActionResult<TipoServicio>> Post([FromBody] TipoServicio tipoServicio)
        {
            if (tipoServicio == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.TipoServicio.Add(tipoServicio);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetTipoServicio", new { id = tipoServicio.Id }, tipoServicio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TipoServicio tipoServicio)
        {
            if (id != tipoServicio.Id) return BadRequest();

            if (tipoServicio == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(tipoServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TipoServicioExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipo = await _context.TipoServicio.FindAsync(id);
            if (tipo == null) return NotFound();

            _context.TipoServicio.Remove(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> TipoServicioExists(int id)
        {
            return await _context.TipoServicio.AnyAsync(e => e.Id == id);
        }
    }
}
