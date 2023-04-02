using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TAtributosGrupo
    {
        public TAtributosGrupo()
        {
            TAtributos = new HashSet<TAtributo>();
        }

        public int IdGrupo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TAtributo> TAtributos { get; set; }
    }
}
