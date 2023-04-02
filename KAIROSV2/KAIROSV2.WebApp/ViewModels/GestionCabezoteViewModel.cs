using KAIROSV2.Business.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionCabezoteViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }
        
        [Required(ErrorMessage = "La placa es obligatoria")]
        [MinLength(6, ErrorMessage = "La longitud son de 6 caracteres")]
        public string Placa { get; set; }

        public bool Lectura { get; set; }

        public void AsignarCabezote(TCabezote cabezote)
        {
            Placa = cabezote?.PlacaCabezote;

        }

        public TCabezote ExtraerCabezote()
        {
            var cabezote = new TCabezote()
            {
                PlacaCabezote = Placa,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };

            return cabezote;

        }

    }
}
