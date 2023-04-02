using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TDespacho
    {
        public TDespacho()
        {
            TDespachosComponentes = new HashSet<TDespachosComponente>();
        }

        public string Id_Despacho { get; set; }
        public string Id_Terminal { get; set; }
        public string Id_Compañia { get; set; }
        public string No_Orden { get; set; }
        public string Id_Producto_Despacho { get; set; }
        public int Compartimento { get; set; }
        public DateTime Fecha_Final_Despacho { get; set; }
        public long? Id_Corte { get; set; }
        public int? Sold_To { get; set; }
        public int? Cedula_Conductor { get; set; }
        public string Placa_Cabezote { get; set; }
        public string Placa_Trailer { get; set; }
        public double Volumen_Ordenado { get; set; }
        public double Volumen_Cargado { get; set; }
        public int Modo { get; set; }
        public bool Estado_Kairos { get; set; }
        public string Observaciones { get; set; }        
        public string Editado_Por { get; set; }
        public DateTime Ultima_Edicion { get; set; }

        public virtual TDespachosModos IdModoNavigation { get; set; }
        public virtual TTerminal IdTerminalNavigation { get; set; }
        public virtual TDespachosTAS IdDespachosTASNavigation { get; set; }
        public virtual ICollection<TDespachosComponente> TDespachosComponentes { get; set; }
        


    }
}
