using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los permisos del sistema
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para los permisos, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir.
    /// </remarks>
    public class PermisosManager : ManagerBase, IPermisosManager
    {
        private readonly IPermisosRepository _permisosRepository;
        private readonly IRolesPermisosRepository _rolesPermisosRepository;
        private readonly IPermisosEngine _permisosEngine;

        public PermisosManager(IPermisosRepository permisosRepository, 
            IPermisosEngine permisosEngine, IRolesPermisosRepository rolesPermisosRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _permisosRepository = permisosRepository;
            _rolesPermisosRepository = rolesPermisosRepository;
            _permisosEngine = permisosEngine;
        }

        /// <summary>
        /// Obtiene la jerarquia de permisos del sistema para un rol especifico.
        /// </summary>
        /// <param name="idRol">Id de rol</param>
        /// <returns>Jerarquia de permisos para un rol</returns>
        public TUPermiso GetPermissionsHierarchyByRol(string idRol)
        {
            var permisos = _permisosRepository.GetPermissionByRol(idRol);
            var permisoApp = permisos.FirstOrDefault(e => e.IdPermisoPadre == null);

            return _permisosEngine.CreatePermissionHierarchy(permisoApp, permisos);
        }

        public PermisosDTO ObtenerBaseJerarquiaPermisos()
        {
            var permisos = _permisosRepository.Get();
            var permisoApp = permisos.FirstOrDefault(e => e.IdPermisoPadre == null);

            var permisoDTO = _permisosEngine.CreatePermissionDTOHierarchy(permisoApp, permisos);

            return permisoDTO;
        }

        public PermisosDTO ObtenerBaseJerarquiaPermisosRol(string idRol)
        {
            var permisos = _permisosRepository.Get();
            var permisoApp = permisos.FirstOrDefault(e => e.IdPermisoPadre == null);
            var basePermisosRol = _rolesPermisosRepository.Get(idRol);

            var permisoDTO = _permisosEngine.CreatePermissionDTOHierarchy(permisoApp, permisos, basePermisosRol);

            return permisoDTO;
        }

        public void AgregarNuevosPermisosParaRol(string idRol, PermisosDTO permiso)
        {
            var listadoRolPermisos = _permisosEngine.GetRolPermisosDePermisoDTO(idRol, permiso);
            _rolesPermisosRepository.RemoveAllByRol(idRol);

            if (listadoRolPermisos != null)
            {
                _rolesPermisosRepository.Add(listadoRolPermisos);
                LogInformacion(LogAcciones.Insertar, "Administración", "Roles", "Roles", "T_U_Roles_Permisos", $"Rol {idRol}, {(listadoRolPermisos?.Count())} permisos creados");
            }
        }

        public void BorrarPermisosParaUsuario(string idRol)
        {
            _rolesPermisosRepository.RemoveAllByRol(idRol);
            LogInformacion(LogAcciones.Eliminar, "Administración", "Roles", "Roles", "T_U_Roles_Permisos", $"Rol {idRol}, eliminar todos los permisos");
        }
    }
}
