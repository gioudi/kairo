using KAIROSV2.Business.Common.Exceptions;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Data.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers
{
    public class RolesManager : ManagerBase, IRolesManager
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IRolesPermisosRepository _rolesPermisosRepository;
        public RolesManager(IRolesRepository rolesRepository,
            IRolesPermisosRepository rolesPermisosRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _rolesRepository = rolesRepository;
            _rolesPermisosRepository = rolesPermisosRepository;
        }
        public IEnumerable<TURole> ObtenerRoles()
        {
            return _rolesRepository.Get();
        }

        public TURole ObtenerRol(string idRol)
        {
            return _rolesRepository.Get(idRol);
        }

        public bool BorrarRol(string idRol)
        {
            var result = false;

            if (idRol == "Administrador")
                throw new ValidationException("No es posible eliminar este Rol");

            if(_rolesRepository.Exists(idRol))
            {
                if (_rolesRepository.RolHasAssociatedUsers(idRol))
                    throw new ValidationException("El rol tiene usuarios asociados.");

                _rolesPermisosRepository.RemoveAllByRol(idRol);
                _rolesRepository.Remove(idRol);
                result = true;
                LogInformacion(LogAcciones.Eliminar, "Administración", "Roles", "Roles", "T_U_Roles", $"Rol {idRol} y sus permisos asociados eliminados.");
            }

            return result;
        }

        public bool CrearRol(TURole rol)
        {
            if (_rolesRepository.Exists(rol.IdRol))
                return false;
            else
            {
                _rolesRepository.Add(rol);
                LogInformacion(LogAcciones.Insertar, "Administración", "Roles", "Roles", "T_U_Roles", $"Rol {rol?.IdRol} creado");
            }

            return true;
        }

        public bool ActualizarRol(TURole rol)
        {
            if (!_rolesRepository.Exists(rol.IdRol))
                return false;
            else
            {
                _rolesRepository.Update(rol);
                LogInformacion(LogAcciones.Actualizar, "Administración", "Roles", "Roles", "T_U_Roles", $"Rol {rol?.IdRol} actualizado");
            }

            return true;
        }
    }
}
