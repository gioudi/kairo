using System;

namespace KAIROSV2.WebApp.Models
{
    public class LogViewModel
    {
        public long Id { get; set; }
        public DateTime FechaEvento { get; set; }
        public string IdUsuario { get; set; }
        public string Aplicacion { get; set; }
        public string Area { get; set; }
        public string Seccion { get; set; }
        public string Accion { get; set; }
        public string Objetivo { get; set; }
        public string Entidad { get; set; }
        public string Prioridad { get; set; }
        public string Comentario { get; set; }
    }
}
