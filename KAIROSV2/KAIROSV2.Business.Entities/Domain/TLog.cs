using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TLog
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime FechaEvento { get; set; }
        public string IdUsuario { get; set; }
        public string Aplicacion { get; set; }
        public string Area { get; set; }
        public string Seccion { get; set; }
        public int Accion { get; set; }
        public string Objetivo { get; set; }
        public string Entidad { get; set; }
        public int Prioridad { get; set; }
        public string Comentario { get; set; }
        public virtual TLogPrioridades IdLogPrioridadesNavigation { get; set; }
        public virtual TLogAcciones IdLogAccionesNavigation { get; set; }


    }
}
