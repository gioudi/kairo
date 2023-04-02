using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TLogPrioridades
    {
        public TLogPrioridades()
        {
            TLogs = new HashSet<TLog>();
        }
        public int Id { get; set; }        
        public string Prioridad { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public virtual ICollection<TLog> TLogs { get; set; }
    }
}
