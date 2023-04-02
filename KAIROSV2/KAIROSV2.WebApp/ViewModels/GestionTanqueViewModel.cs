using KAIROSV2.Business.Entities;
using KAIROSV2.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionTanqueViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }

        [Required(ErrorMessage = "Tanque es obligatorio")]
        [MaxLength(25, ErrorMessage = "La longitud máxima son 25 caracteres")]
        public string IdTanque { get; set; }

        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string IdTerminal { get; set; }

        [Required(ErrorMessage = "Terminal es obligatorio")]
        [MaxLength(80, ErrorMessage = "La longitud máxima son 80 caracteres")]
        public string Terminal { get; set; }

        [Required(ErrorMessage = "Producto es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Producto { get; set; }

        [Required(ErrorMessage = "Estado es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Clase de tanque es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string ClaseTanque { get; set; }

        [Required(ErrorMessage = "Tipo tanque es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string TipoTanque { get; set; }

        [Required(ErrorMessage = "Capacidad nominal es obligatorio")]
        public int CapacidadNominal { get; set; }

        [Required(ErrorMessage = "Capacidad operativa es obligatorio")]
        public int CapacidadOperativa { get; set; }

        [Required(ErrorMessage = "Altura es obligatoria")]
        public int AlturaMaximaAforo { get; set; }

        [Required(ErrorMessage = "Volumen es obligatorio")]
        public double VolumenNoBombeable { get; set; }

        [Required(ErrorMessage = "Aforado por es obligatorio")]
        [MaxLength(250, ErrorMessage = "La longitud máxima son 250 caracteres")]
        public string AforadoPor { get; set; }

        [Required(ErrorMessage = "Fecha de aforo es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FechaAforo { get; set; }

        [MaxLength(120, ErrorMessage = "La longitud máxima son 120 caracteres")]
        public string Observaciones { get; set; }

        public bool PantallaFlotante { get; set; }

        [Required(ErrorMessage = "Densidad es obligatorio")]
        public double DensidadAforo { get; set; }

        [Required(ErrorMessage = "Galones por grado es obligatorio")]
        public double GalonesPorGrado { get; set; }

        [Required(ErrorMessage = "Nivel correción Inicial es obligatorio")]
        public int NivelCorreccionInicial { get; set; }

        [Required(ErrorMessage = "Nivel correción final es obligatorio")]
        public int NivelCorreccionFinal { get; set; }

        public bool Lectura { get; set; }
        //public IEnumerable<HelperSelectInt> EstadosTanque { get; set; }
        //public IEnumerable<HelperSelectString> Terminales { get; set; }
        //public IEnumerable<HelperSelectString> Productos { get; set; }

        public IEnumerable<string> EstadosTanque { get; set; }
        public IEnumerable<string> Terminales { get; set; }
        public IEnumerable<string> Productos { get; set; }
        public IEnumerable<string> Tipos { get; set; }
        public IEnumerable<string> Clases { get; set; }


        public GestionTanqueViewModel()
        {
            Tipos = new List<string>() { "VERTICAL", "HORIZONTAL", "BARRIL" };
            Clases = new List<string>() { "FISICO", "VIRTUAL" };
        }



        public void AsignarTanque(TTanque _Tanque)
        {
            IdTanque = _Tanque?.IdTanque;
            //IdProducto = _Tanque?.IdProducto;
            TipoTanque = _Tanque?.TipoTanque;
            ClaseTanque = _Tanque?.ClaseTanque;
            //IdEstado = _Tanque.IdEstado;
            CapacidadNominal = _Tanque.CapacidadNominal;
            CapacidadOperativa = _Tanque.CapacidadOperativa;
            //Altura_Maxima_Aforo = _Tanque.Altura_Maxima_Aforo;
            PantallaFlotante = _Tanque.PantallaFlotante;
            VolumenNoBombeable = _Tanque.VolumenNoBombeable;
            AforadoPor = _Tanque?.AforadoPor;
            FechaAforo = _Tanque.FechaAforo;
            Observaciones = _Tanque?.Observaciones;



        }

        public TTanque ExtraerTanque(string Id_Terminal , string Id_Producto , int Id_Estado)
        {
            var _Tanque = new TTanque()
            {
                IdTanque = IdTanque,
                IdTerminal = Id_Terminal,
                IdProducto = Id_Producto,
                TipoTanque = TipoTanque,
                ClaseTanque = ClaseTanque,
                IdEstado = Id_Estado,
                CapacidadNominal = Convert.ToInt32( CapacidadNominal),
                CapacidadOperativa = Convert.ToInt32(CapacidadOperativa),
                AlturaMaximaAforo = Convert.ToInt32(AlturaMaximaAforo),
                PantallaFlotante = PantallaFlotante,
                VolumenNoBombeable = VolumenNoBombeable,
                AforadoPor = AforadoPor,
                FechaAforo = FechaAforo,
                Observaciones = Observaciones,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
                
            };    


            if (PantallaFlotante)
            {
                _Tanque.IdTanquesPantallaFlotanteNavigation = new TTanquesPantallaFlotante()
                {
                    DensidadAforo = DensidadAforo,
                    EditadoPor = "Admin",
                    GalonesPorGrado = GalonesPorGrado,
                    IdTanque = IdTanque,
                    IdTerminal = Id_Terminal,
                    NivelCorreccionFinal = Convert.ToInt32(NivelCorreccionFinal),
                    NivelCorreccionInicial = Convert.ToInt32(NivelCorreccionInicial),
                    UltimaEdicion = DateTime.Now
                };
            }

            return _Tanque;
                
        }

    }
}
