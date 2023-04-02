using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TLineasEstado
    {
        public TLineasEstado()
        {
            TLineas = new HashSet<TLinea>();
        }

        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public string ColorHex { get; set; }
        public string Icono { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }

        public virtual ICollection<TLinea> TLineas { get; set; }
    }
}
