using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionLIneaViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }

        [Required(ErrorMessage = "Id Linea es obligatorio")]
        [MaxLength(25, ErrorMessage = "La longitud máxima son 25 caracteres")]
        public string IdLinea { get; set; }

        public TLinea Linea { get; set; }

        //[Required(ErrorMessage = "Id Terminal es obligatorio")]
        //public string IdTerminal { get; set; }

        [Required(ErrorMessage = "Terminal es obligatoria")]
        [MaxLength(80, ErrorMessage = "La longitud máxima son 80 caracteres")]
        public string Terminal { get; set; }

        public IEnumerable<string> Terminales { get; set; }

        //[Required (ErrorMessage ="Id Producto es obligatorio")]
        //public string IdProducto { get; set; }

        [Required(ErrorMessage ="Producto es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Producto { get; set; }

        public int IdEstado { get; set; }

        [Required(ErrorMessage = "Estado es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Estado { get; set; }

        public IEnumerable<string> EstadosLinea { get; set; }

        [Required(ErrorMessage = "Capacidad es obligatorio")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "Densidad Aforo es obligatorio")]
        public double DensidadAforo { get; set; }

        [MaxLength(250, ErrorMessage = "La longitud máxima son 250 caracteres")]
        public string Observaciones { get; set; }

        public bool Lectura { get; set; }

        public IEnumerable<string> Productos { get; set; }
        public void AsignarLinea(TLinea _linea)
        {
            IdLinea = _linea.IdLinea;
            Capacidad = _linea.Capacidad;
            DensidadAforo = _linea.DensidadAforo;
            Observaciones = _linea.Observaciones;

        }

        public TLinea ExtraerLinea(string Id_Terminal, string Id_Producto, int Id_Estado)
        {
            var _linea = new TLinea()
            {
                IdLinea = IdLinea,
                IdProducto= Id_Producto,     
                IdTerminal = Id_Terminal,
                Id_Estado = Id_Estado,
                Capacidad =Capacidad,
                DensidadAforo=DensidadAforo,
                Observaciones=Observaciones,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };
            return _linea;

        }
    }
}
