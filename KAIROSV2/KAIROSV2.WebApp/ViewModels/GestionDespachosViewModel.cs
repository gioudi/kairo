using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionDespachosViewModel
    {
        public GestionDespachosViewModel()
        {
            Terminales = new List<SelectListItem>();
            Compañias = new List<SelectListItem>();
            Productos = new List<SelectListItem>();            
            Sold_Tos = new List<SelectListItem>();
            Componentes = new List<DespachosComponentesViewModel>();            
        }

        public string Titulo { get; set; }
        public string Accion { get; set; }
        public bool Lectura { get; set; }

        public string Id_Despacho { get; set; }

        [Required(ErrorMessage = "La Terminal es obligatoria")]
        public string Terminal { get; set; }

        [Required(ErrorMessage = "La Compañia es obligatoria")]
        public string Compañia { get; set; }

        [Required(ErrorMessage = "La fecha de despacho es obligatoria")]
        public DateTime FechaDespacho { get; set; }

        [Required(ErrorMessage = "El número de Orden es obligatorio")]
        [MaxLength(40, ErrorMessage = "La longitud máxima son 32 caracteres")]
        public string No_Orden { get; set; }

        [Required(ErrorMessage = "El código de producto es obligatorio")]
        [MaxLength(15, ErrorMessage = "La longitud máxima son 15 caracteres")]
        public string IdProducto { get; set; }

        [Required(ErrorMessage = "La Placa Cabezote es obligatoria")]
        [MaxLength(8, ErrorMessage = "La longitud máxima son 8 caracteres")]
        public string Placa_Cabezote { get; set; }

        [Required(ErrorMessage = "La Placa Trailer es obligatoria")]
        [MaxLength(8, ErrorMessage = "La longitud máxima son 8 caracteres")]
        public string Placa_Trailer { get; set; }

        [Required(ErrorMessage = "El Compartimento es obligatorio")]
        public int Compartimento { get; set; }

        [Required(ErrorMessage = "El código de cliente es obligatorio")]
        [Range(1, 99999999, ErrorMessage = "La longitud máxima son 8 caracteres")]
        public int? Sold_To { get; set; }

        [Required(ErrorMessage = "El cedula de conductor es obligatorio")]
        [Range(1, 2000000000, ErrorMessage ="Rango maximo de 2 mil millones")]
        public int? Cedula_Conductor { get; set; }
        

        [Required(ErrorMessage = "El Volumen Ordenado es obligatorio")]
        public double Volumen_Ordenado { get; set; }

        [Required(ErrorMessage = "El Volumen Cargado es obligatorio")]
        public double Volumen_Cargado { get; set; }

        [Required(ErrorMessage = "El Observaciones es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Observaciones { get; set; }

        public List<SelectListItem> Terminales { get; set; }
        public List<SelectListItem> Compañias { get; set; }
        public List<SelectListItem> Productos { get; set; }
        public List<SelectListItem> Sold_Tos { get; set; }
        public List<DespachosComponentesViewModel> Componentes { get; set; }

        public TDespacho ExtraerDespacho()
        {
            var _Despacho = new TDespacho()
            {
                Cedula_Conductor = Cedula_Conductor,
                Compartimento = Compartimento,
                Editado_Por = "Admin",
                Estado_Kairos = true,
                Fecha_Final_Despacho = FechaDespacho,
                Id_Compañia = Compañia,
                Id_Despacho = Id_Despacho,
                Modo = 2,
                Id_Terminal = Terminal,
                No_Orden = No_Orden,
                Id_Producto_Despacho = IdProducto,
                Observaciones = Observaciones,
                Placa_Cabezote = Placa_Cabezote,
                Placa_Trailer = Placa_Trailer,
                Sold_To = Sold_To,
                Ultima_Edicion = DateTime.Now,
                Volumen_Cargado = Volumen_Cargado,
                Volumen_Ordenado = Volumen_Ordenado
            };

            if (Componentes != null)
            {
                _Despacho.TDespachosComponentes = new List<TDespachosComponente>();

                foreach (var componente in Componentes)
                {
                    var nuevoComponente = new TDespachosComponente()
                    {
                        Compartimento = Compartimento,
                        Contador = componente.Contador,
                        Densidad = componente.Densidad,
                        EditadoPor = "Admin",
                        Id_Despacho = Terminal + Compañia + No_Orden + IdProducto + Compartimento,
                        No_Orden = No_Orden,
                        Ship_To = componente.Ship_To,
                        Tanque = componente.Tanque,
                        Temperatura = componente.Temperatura,
                        UltimaEdicion = DateTime.Now,
                        Volumen_Bruto = componente.Volumen_Bruto,
                        Volumen_Neto = componente.Volumen_Neto,
                        Id_Producto_Componente = componente.Producto_Componente,

                    };

                    _Despacho.TDespachosComponentes.Add(nuevoComponente);

                }
            }

            return _Despacho;
            
        }
                        
    }

}
