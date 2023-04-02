using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace KAIROSV2.Business.Entities.DTOs
{
    public class RecetaComponenteDTO
    {
        private double _Porcentaje;
        public string IdReceta { get; set; }
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Tipo { get; set; }
        public int Posicion { get; set; }
        [Required(ErrorMessage = "Es necesario seleccionar un producto para el componente")]
        public string IdComponente { get; set; }
        [Required(ErrorMessage = "La proporción del componente es obligatorio")]
        [Range(1, 1000000, ErrorMessage = "El valor de proporción del componente tiene que ser mayor a cero y menor a 1000000")]
        public double ProporcionComponente { get; set; }
        public double Porcentaje
        {
            get { return ((ProporcionComponente / 1000000) * 100); }
            set { _Porcentaje = value; }
        }
    }
}