using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TDespachosTAS
    {        

        public string Id_Despacho { get; set; }
        public int Id_TAS { get; set; }

        public virtual TDespacho IdDespachoNavigation { get; set; }
        public virtual TTAS IdTASNavigation { get; set; }

    }
}
