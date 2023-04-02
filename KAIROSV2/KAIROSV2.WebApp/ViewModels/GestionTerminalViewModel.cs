using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.Support.Util;
using KAIROSV2.WebApp.Support.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionTerminalViewModel
    {

        public GestionTerminalViewModel()
        {
            EncabezadosTerminales = new List<string>() { "Id Terminal", "Terminal", "Estado", "Area", "Superintendente", "Dirección", "Teléfono", "Centro Costo", "Conjunta", "Ventas Terceros", "Tipo Informe Terceros", "Poliducto", "Compañia Operadora" };
            EncabezadosCompañias = new List<string>() { "", "Id Compañia", "Nombre", "Sales Organization", "Distribution Channel", "Division", "Supplier Type", "SICOM" };
            EncabezadosTerminalCompañias = new List<string>() { "" , "Nombre", "Estado", "SICOM", "Socia", "Compañia Agrupadora", "Porcentaje Propiedad" };
            EncabezadosProductos = new List<string>() { "", "Id Producto", "Nombre ERP", "Nombre Corto", "SICOM", "Id Clase", "Id Tipo", "Estado" };
            EncabezadosTerminalCompañiasProductos = new List<string>() { "", "Id Producto", "Id Terminal", "Id Compañia", "Participa en Variaciones", "Id Compañia asume" };
        }

        public IEnumerable<string> EncabezadosTerminales { get; set; }
        public IEnumerable<string> EncabezadosCompañias { get; set; }
        public IEnumerable<string> EncabezadosTerminalCompañias { get; set; }
        public IEnumerable<string> EncabezadosProductos { get; set; }
        public IEnumerable<string> EncabezadosTerminalCompañiasProductos { get; set; }

        public string Titulo { get; set; }

        public string Accion { get; set; }

        [Required(ErrorMessage = "Id terminal es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string IdTerminal { get; set; }

        [Required(ErrorMessage = "Nombre terminal es obligatorio")]
        [MinLength(5, ErrorMessage = "La longitud minima son 5 caracteres")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Nombre { get; set; }

        public TTerminal Terminal { get; set; }

        [Required(ErrorMessage = "Estado es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Área es obligatoria")]
        [MaxLength(25, ErrorMessage = "La longitud máxima son 25 caracteres")]
        public string Area { get; set; }

        [Required(ErrorMessage = "Superintendente es obligatorio")]
        [MaxLength(40, ErrorMessage = "La longitud máxima son 40 caracteres")]
        public string Superintendente { get; set; }

        [Required(ErrorMessage = "Dirección es obligatoria")]
        [MinLength(10, ErrorMessage = "La longitud minima son 10 caracteres")]
        [MaxLength(80, ErrorMessage = "La longitud máxima son 80 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Teléfono es obligatorio")]
        [MaxLength(25, ErrorMessage = "La longitud máxima son 25 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Centro de costo es obligatorio")]
        [MinLength(3, ErrorMessage = "La longitud minima son 3 caracteres")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string CentroCosto { get; set; }

        [Required(ErrorMessage = "Tipo de informe es obligatorio")]
        [MinLength(4, ErrorMessage = "La longitud minima son 4 caracteres")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string TipoInformeTerceros { get; set; }

        [Required(ErrorMessage = "Poliducto es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string Poliducto { get; set; }

        [Required(ErrorMessage = "Id compañía es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string IdCompañiaOperadora { get; set; }

        public bool Conjunta { get; set; }

        public bool VentasTerceros { get; set; }

        public IEnumerable<TCompañia> Compañias { get; set; }
        
        public IEnumerable<ProductoTerminalDto> Productos { get; set; }

        public IEnumerable<TTerminalCompañiasProducto> TerminalCompañiasProductos { get; set; }

        public IEnumerable<string> EstadosTerminal { get; set; }

        public IEnumerable<string> Areas { get; set; }

        public bool Lectura { get; set; }

        public IEnumerable<TerminalCompañiaViewModel> TerminalCompañias { get; set; }
        
        public void AsignarTerminal(TTerminal _Terminal)
        {
            IdTerminal = _Terminal?.IdTerminal;
            Terminal = _Terminal;
            Nombre = _Terminal?.Terminal;
            CentroCosto = _Terminal?.CentroCosto;
            Conjunta = _Terminal.Conjunta;
            Direccion = _Terminal?.Direccion;
            Area = _Terminal?.IdArea;
            Estado = _Terminal?.IdEstado.ToString();
            IdCompañiaOperadora = _Terminal?.IdCompañiaOperadora;
            Poliducto = _Terminal?.Poliducto;
            Superintendente = _Terminal?.Superintendente;
            Telefono = _Terminal?.Telefono;
            VentasTerceros = _Terminal.VentasTerceros;
            TipoInformeTerceros = _Terminal?.TipoInformeTerceros;
        }

        public TTerminal ExtraerTerminal(IEnumerable<TTerminalesEstado> estados, IEnumerable<TArea> areas )
        {
            var _Terminal = new TTerminal()
            {
                IdTerminal = IdTerminal,
                Terminal = Nombre,
                CentroCosto = CentroCosto,
                Conjunta = Conjunta,
                Direccion = Direccion,
                IdArea = areas.FirstOrDefault(e => e.Area == Area).IdArea,                
                IdEstado = estados.FirstOrDefault(i => i.Descripcion == Estado).IdEstado,
                IdCompañiaOperadora = IdCompañiaOperadora,
                Poliducto = Poliducto,
                Superintendente = Superintendente,
                Telefono = Telefono,
                VentasTerceros = VentasTerceros,
                TipoInformeTerceros = TipoInformeTerceros,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now,
                
            };           

            return _Terminal;
                
        }

    }
}
