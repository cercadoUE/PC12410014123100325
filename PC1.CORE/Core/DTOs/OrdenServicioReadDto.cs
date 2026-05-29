using System;

namespace PC1.CORE.Core.DTOs
{
    public class OrdenServicioReadDto
    {
        public int Id { get; set; }

        // Fecha en formato ISO yyyy-MM-dd
        public string FechaIngreso { get; set; } = string.Empty;

        public string DescripcionProblema { get; set; } = string.Empty;

        public decimal? CostoEstimado { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int VehiculoId { get; set; }

        public int TipoServicioId { get; set; }
    }
}
