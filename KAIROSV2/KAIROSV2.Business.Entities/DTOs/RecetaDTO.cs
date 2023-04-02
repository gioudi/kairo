using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class RecetaDTO
    {
        public bool Asignada { get; set; }
        [Required(ErrorMessage = "El id de la receta es obligatorio")]
        public string IdReceta { get; set; }
        public string IdRecetaCurrent { get; set; }
        public string IdProducto { get; set; }
        [Required(ErrorMessage = "Los componentes de una receta son obligatorios")]
        public List<RecetaComponenteDTO> Componentes { get; set; }
    }
}