using System;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class VigenciaRecetaDTO
    {
        public string IdProducto { get; set; }
        public string IdTerminal { get; set; }
        public string IdReceta { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public TimeSpan? HoraExpiracion { get; set; }
    }
}