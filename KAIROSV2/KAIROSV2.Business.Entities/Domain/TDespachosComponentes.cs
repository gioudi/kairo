using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TDespachosComponente
    {
        public string Id_Despacho { get; set; }
        public string No_Orden { get; set; }
        public int Ship_To { get; set; }
        public string Id_Producto_Componente { get; set; }
        public int Compartimento { get; set; }
        public string Tanque { get; set; }
        public string Contador { get; set; }
        public double Volumen_Bruto { get; set; }
        public double Volumen_Neto { get; set; }
        public double Temperatura { get; set; }
        public double Densidad { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        

        public virtual TDespacho IdDespachoNavigation { get; set; }
        
    }
}
