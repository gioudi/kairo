using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IRolesManager
    {
        IEnumerable<TURole> ObtenerRoles();
        TURole ObtenerRol(string idRol);
        bool BorrarRol(string idRol);
        bool CrearRol(TURole rol);
        bool ActualizarRol(TURole rol);
    }
}
