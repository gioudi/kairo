using KAIROSV2.Business.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionContadorViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }

        [Required(ErrorMessage = "Nombre del Contador es obligatorio")]
        [MaxLength(20, ErrorMessage = "La longitud maxima son 20 caracteres")]
        public string IdContador { get; set; }

        public bool Lectura { get; set; }

        public void AsignarContador(TContador Contador)
        {
            IdContador = Contador?.IdContador;

        }

        public TContador ExtraerContador()
        {
            var Contador = new TContador()
            {
                IdContador = IdContador,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };

            return Contador;

        }

    }
}
