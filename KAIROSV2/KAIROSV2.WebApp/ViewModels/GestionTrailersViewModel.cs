using KAIROSV2.Business.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionTrailerViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }

        [Required(ErrorMessage = "La placa del Trailer es obligatorio, por favor digitela")]
        [MinLength(6, ErrorMessage = "La longitud son de 6 caracteres")]
        public string Placa { get; set; }

        public bool Lectura { get; set; }

        public void AsignarTrailer(TTrailer trailer)
        {
            Placa = trailer?.PlacaTrailer;

        }

        public TTrailer ExtraerTrailer()
        {
            var trailer = new TTrailer()
            {
                PlacaTrailer = Placa,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };

            return trailer;

        }

    }
}
