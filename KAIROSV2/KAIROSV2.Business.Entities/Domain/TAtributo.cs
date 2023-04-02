using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TAtributo
    {
        public TAtributo()
        {
            TProductosAtributos = new HashSet<TProductosAtributo>();
        }

        public int IdAtributo { get; set; }
        public string Descripcion { get; set; }
        public int Grupo { get; set; }
        public int TipoDato { get; set; }
        public string Restricciones { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TAtributosGrupo GrupoNavigation { get; set; }
        public virtual TAtributosTipo TipoDatoNavigation { get; set; }
        public virtual ICollection<TProductosAtributo> TProductosAtributos { get; set; }
    }
}
