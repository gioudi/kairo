using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TUUsuariosTerminalCompañia
    {
        public string IdUsuario { get; set; }
        public string IdTerminal { get; set; }
        public string IdCompañia { get; set; }
        public string Estado { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }

        public virtual TTerminalCompañia Id { get; set; }
        public virtual TUUsuario IdUsuarioNavigation { get; set; }
    }
}
