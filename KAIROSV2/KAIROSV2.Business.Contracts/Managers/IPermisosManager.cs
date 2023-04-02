using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IPermisosManager
    {
        TUPermiso GetPermissionsHierarchyByRol(string idRol);
        PermisosDTO ObtenerBaseJerarquiaPermisos();
        PermisosDTO ObtenerBaseJerarquiaPermisosRol(string idRol);
        void AgregarNuevosPermisosParaRol(string idRol, PermisosDTO permiso);
        void BorrarPermisosParaUsuario(string idRol);
    }
}
