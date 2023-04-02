using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TTASCortes
    {
        public long Id_Corte { get; set; }
        public string Id_Terminal { get; set; }
        public DateTime Fecha_Corte { get; set; }
        public DateTime? Fecha_Cierre_Kairos { get; set; }
        public string Id_TAS_Folio { get; set; }
        public int Id_TAS { get; set; }
        public string Editado_Por { get; set; }
        public DateTime Ultima_Edicion { get; set; }

        public virtual TTAS IdTASNavigation { get; set; }
        public virtual TTerminal IdTerminalNavigation { get; set; }

    }
}

