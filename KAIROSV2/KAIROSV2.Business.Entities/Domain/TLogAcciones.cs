using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TLogAcciones
    {
        public TLogAcciones()
        {
            TLogs = new HashSet<TLog>();
        }
        public int Id { get; set; }        
        public string Accion { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public virtual ICollection<TLog> TLogs { get; set; }
    }
}
