using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TTAS
    {
        public TTAS()
        {
            TDespachosTAS = new HashSet<TDespachosTAS>();
            TTASCortes = new HashSet<TTASCortes>();
            //TDespachos = new HashSet<TDespacho>();
        }
        
        [Key]
        public int Id_TAS { get; set; }
        public int Id_Modo { get; set; }
        public string Descripcion { get; set; }


        public virtual TDespachosModos IdDespachosModosNavigation { get; set; }
        public virtual ICollection<TDespachosTAS> TDespachosTAS { get; set; }
        public virtual ICollection<TTASCortes> TTASCortes { get; set; }
        //public virtual ICollection<TDespacho> TDespachos { get; set; }

    }
}
