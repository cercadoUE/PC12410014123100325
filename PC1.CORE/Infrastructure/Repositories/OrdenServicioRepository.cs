using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PC1.CORE.Core.DTOs;
using PC1.CORE.Core.Entities;
using PC1.CORE.Core.Interfaces;

namespace PC1.CORE.Infrastructure.Repositories
{
    public class OrdenServicioRepository : IOrdenServicioRepository
    {
        private readonly TallerMecanicoDbContext _context;

        public OrdenServicioRepository(TallerMecanicoDbContext context)
        {
            _context = context;
        }

        public async Task<OrdenServicioReadDto> CreateAsync(OrdenServicioCreateDto dto)
        {
            var entidad = new OrdenServicio
            {
                FechaIngreso = DateOnly.Parse(dto.FechaIngreso),
                DescripcionProblema = dto.DescripcionProblema,
                CostoEstimado = dto.CostoEstimado,
                Estado = dto.Estado,
                VehiculoId = dto.VehiculoId,
                TipoServicioId = dto.TipoServicioId
            };

            _context.OrdenServicio.Add(entidad);
            await _context.SaveChangesAsync();

            return new OrdenServicioReadDto
            {
                Id = entidad.Id,
                FechaIngreso = entidad.FechaIngreso.ToString("yyyy-MM-dd"),
                DescripcionProblema = entidad.DescripcionProblema,
                CostoEstimado = entidad.CostoEstimado,
                Estado = entidad.Estado,
                VehiculoId = entidad.VehiculoId,
                TipoServicioId = entidad.TipoServicioId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entidad = await _context.OrdenServicio.FindAsync(id);
            if (entidad == null) return false;

            _context.OrdenServicio.Remove(entidad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrdenServicioReadDto>> GetAllAsync()
        {
            return await _context.OrdenServicio
                .AsNoTracking()
                .Select(e => new OrdenServicioReadDto
                {
                    Id = e.Id,
                    FechaIngreso = e.FechaIngreso.ToString("yyyy-MM-dd"),
                    DescripcionProblema = e.DescripcionProblema,
                    CostoEstimado = e.CostoEstimado,
                    Estado = e.Estado,
                    VehiculoId = e.VehiculoId,
                    TipoServicioId = e.TipoServicioId
                })
                .ToListAsync();
        }

        public async Task<OrdenServicioReadDto?> GetByIdAsync(int id)
        {
            var e = await _context.OrdenServicio
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new OrdenServicioReadDto
                {
                    Id = x.Id,
                    FechaIngreso = x.FechaIngreso.ToString("yyyy-MM-dd"),
                    DescripcionProblema = x.DescripcionProblema,
                    CostoEstimado = x.CostoEstimado,
                    Estado = x.Estado,
                    VehiculoId = x.VehiculoId,
                    TipoServicioId = x.TipoServicioId
                })
                .FirstOrDefaultAsync();

            return e;
        }

        public async Task<bool> UpdateAsync(int id, OrdenServicioCreateDto dto)
        {
            var entidad = await _context.OrdenServicio.FindAsync(id);
            if (entidad == null) return false;

            entidad.FechaIngreso = DateOnly.Parse(dto.FechaIngreso);
            entidad.DescripcionProblema = dto.DescripcionProblema;
            entidad.CostoEstimado = dto.CostoEstimado;
            entidad.Estado = dto.Estado;
            entidad.VehiculoId = dto.VehiculoId;
            entidad.TipoServicioId = dto.TipoServicioId;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
    }
}
