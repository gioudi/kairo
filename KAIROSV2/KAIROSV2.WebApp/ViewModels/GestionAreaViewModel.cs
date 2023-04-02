using KAIROSV2.Business.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionAreaViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }
                
        [Required(ErrorMessage = "Id Area es obligatorio")]
        public string IdArea { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [MinLength(2, ErrorMessage = "La longitud minima son 2 caracteres")]
        [MaxLength(25, ErrorMessage = "La longitud máxima son 25 caracteres")]
        public string Area { get; set; }
                
        public bool Lectura { get; set; }

        public void AsignarArea(TArea _area)
        {
            IdArea = _area?.IdArea;
            Area = _area?.Area;
            
        }

        public TArea ExtraerArea()
        {
            var _area = new TArea()
            {
                IdArea = IdArea,
                Area = Area,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };    
            return _area;
                
        }

    }
}
