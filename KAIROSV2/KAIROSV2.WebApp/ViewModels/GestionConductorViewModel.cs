using KAIROSV2.Business.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionConductorViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }

        [Required(ErrorMessage = "El numero de cedula es obligatorio, por favor digitela")]
        public int Cedula { get; set; }

        [Required(ErrorMessage = "El nombre del conductor es obligatorio, por favor digitelo")]
        [MinLength(4, ErrorMessage = "La longitud minima son 4 caracteres")]
        public string Nombre { get; set; }
                
        
        public void AsignarConductor(TConductor Conductor)
        {
            Cedula = Conductor.Cedula;
            Nombre = Conductor?.Nombre;
        }

        public TConductor ExtraerConductor()
        {
            var Conductor = new TConductor()
            {
                Cedula = Cedula,
                Nombre = Nombre,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now,
                FilaId = 0

            };

            return Conductor;
                
        }

        public bool Lectura { get; set; }

    }
}
