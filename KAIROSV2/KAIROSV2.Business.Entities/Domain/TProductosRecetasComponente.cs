using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TProductosRecetasComponente
    {
        public string IdReceta { get; set; }
        public string IdProducto { get; set; }
        public int Posicion { get; set; }
        public string IdComponente { get; set; }
        public double ProporcionComponente { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TProductosReceta Id { get; set; }
        public virtual TProducto IdComponenteNavigation { get; set; }
    }
}
