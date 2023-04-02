using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TDespachosModos
    {
        public TDespachosModos()
        {
            TTAS = new HashSet<TTAS>();
            TDespacho = new HashSet<TDespacho>();
        }

        public int Id_Modo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TDespacho> TDespacho { get; set; }        
        public virtual ICollection<TTAS> TTAS { get; set; }

    }
}
