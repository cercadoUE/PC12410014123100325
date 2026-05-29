using System.Collections.Generic;
using System.Threading.Tasks;
using PC1.CORE.Core.DTOs;

namespace PC1.CORE.Core.Interfaces
{
    public interface IOrdenServicioRepository
    {
        Task<IEnumerable<OrdenServicioReadDto>> GetAllAsync();
        Task<OrdenServicioReadDto?> GetByIdAsync(int id);
        Task<OrdenServicioReadDto> CreateAsync(OrdenServicioCreateDto dto);
        Task<bool> UpdateAsync(int id, OrdenServicioCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
