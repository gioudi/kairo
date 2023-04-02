using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;

namespace KAIROSV2.Business.Contracts.Engines
{
    public interface IPermisosEngine
    {
        TUPermiso CreatePermissionHierarchy(TUPermiso initialPermission, IEnumerable<TUPermiso> permissions);
        PermisosDTO CreatePermissionDTOHierarchy(TUPermiso initialPermission, IEnumerable<TUPermiso> permissions, IEnumerable<TURolesPermiso> rolePermisos = default);
        IEnumerable<TURolesPermiso> GetRolPermisosDePermisoDTO(string idRol, PermisosDTO permisoDTO);
    }
}
