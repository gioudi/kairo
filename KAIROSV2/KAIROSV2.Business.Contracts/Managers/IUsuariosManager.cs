using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IUsuariosManager
    {
        TUUsuario ObtenerUsuario(string idUsuario);
        IEnumerable<TUUsuario> ObtenerUsuarios();
        bool CrearUsuario(TUUsuario usuario);
        bool ActualizarUsuario(TUUsuario usuario);
    }
}
