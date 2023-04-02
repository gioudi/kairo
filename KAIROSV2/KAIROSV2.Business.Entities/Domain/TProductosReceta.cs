using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TProductosReceta
    {
        public TProductosReceta()
        {
            TProductosRecetasComponentes = new HashSet<TProductosRecetasComponente>();
            TTerminalesProductosReceta = new HashSet<TTerminalesProductosReceta>();
        }

        public string IdReceta { get; set; }
        public string IdProducto { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TProducto IdProductoNavigation { get; set; }
        public virtual ICollection<TProductosRecetasComponente> TProductosRecetasComponentes { get; set; }
        public virtual ICollection<TTerminalesProductosReceta> TTerminalesProductosReceta { get; set; }
    }
}
