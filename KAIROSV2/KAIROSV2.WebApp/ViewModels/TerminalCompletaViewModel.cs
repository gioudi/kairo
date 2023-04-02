using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class TerminalCompletaViewModel
    {
        public TTerminal Terminal { get; set; }
        public IEnumerable<string> EncabezadosTerminales { get; set; }
        public IEnumerable<TCompañia> Compañias { get; set; }
        public IEnumerable<string> EncabezadosCompañias { get; set; }
        public IEnumerable<TTerminalCompañia> TerminalCompañias { get; set; }
        public IEnumerable<string> EncabezadosTerminalCompañias { get; set; }
        public IEnumerable<TProducto> Productos { get; set; }
        public IEnumerable<string> EncabezadosProductos { get; set; }
        public IEnumerable<TTerminalCompañiasProducto> TerminalCompañiasProductos { get; set; }
        public IEnumerable<string> EncabezadosTerminalCompañiasProductos { get; set; }


        public TerminalCompletaViewModel()
        {
            EncabezadosTerminales = new List<string>() { "Id Terminal", "Terminal", "Estado", "Area", "Superintendente", "Dirección", "Teléfono", "Centro Costo", "Conjunta", "Ventas Terceros", "Tipo Informe Terceros", "Poliducto", "Compañia Operadora" };
            EncabezadosCompañias = new List<string>() { "", "Id Compañia", "Nombre", "Sales Organization", "Distribution Channel", "Division", "Supplier Type", "SICOM"};
            EncabezadosTerminalCompañias = new List<string>() { "", "Id Terminal", "Id Compañia", "Estado", "SICOM", "Socia" , "Compañia Agrupadora", "Porcentaje Propiedad" };
            EncabezadosProductos = new List<string>() { "", "Id Producto", "Nombre ERP", "Nombre Corto", "SICOM", "Id Clase", "Id Tipo", "Estado" };
            EncabezadosTerminalCompañiasProductos = new List<string>() { "", "Id Producto", "Id Terminal", "Id Compañia", "Participa en Variaciones", "Id Compañia asume" };
        }
    }
}

