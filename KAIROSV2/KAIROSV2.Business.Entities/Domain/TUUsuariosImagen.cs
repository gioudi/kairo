using System;
using System.Collections.Generic;

namespace KAIROSV2.Business.Entities
{
    public partial class TUUsuariosImagen
    {
        public string IdUsuario { get; set; }
        public byte[] Imagen { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public virtual TUUsuario IdUsuarioNavigation { get; set; }
    }
}
