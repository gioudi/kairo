using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Entities;
using System.Linq;
using KAIROSV2.Business.Entities.DTOs;

namespace KAIROSV2.Business.Engines
{
    public class PermisosEngine : IPermisosEngine
    {
        /// <summary>
        /// Organiza la jerarquia de permisos dado un permisos inicial y una lista plana de permisos.
        /// </summary>
        /// <remarks>
        /// Revisa la lista de permisos y encuentra el permiso inicial, 
        /// a partir de este punto empieza a iterar en los subpermisos 
        /// encontrando a su vez tambien sus subpermisos para asociarlos a la lista
        /// de permisos hijos de cada permiso hasta el permisos inicial
        /// </remarks>
        /// <param name="initialPermission">Permiso inicial</param>
        /// <param name="permissions">Lista de permisos</param>
        /// <returns>Jerarquia de permisos</returns>
        public TUPermiso CreatePermissionHierarchy(TUPermiso initialPermission, IEnumerable<TUPermiso> permissions)
        {
            Func<TUPermiso, List<TUPermiso>> createNestedList = null;
            createNestedList = (p) =>
            {
                var childPermissions = permissions.Where(e => e.IdPermisoPadre == p.IdPermiso);
                foreach (var permission in childPermissions)
                {
                    permission.InverseIdPermisoPadreNavigation = createNestedList(permission);
                }

                return childPermissions?.ToList();
            };

            if (initialPermission != null)
                initialPermission.InverseIdPermisoPadreNavigation = createNestedList(initialPermission);

            return initialPermission;
        }

        public PermisosDTO CreatePermissionDTOHierarchy(TUPermiso initialPermission, IEnumerable<TUPermiso> permissions, IEnumerable<TURolesPermiso> rolePermisos = default)
        {
            var permisoDTO = AdaptarPermisoAPermisoDTO(initialPermission);
            Func<PermisosDTO, List<PermisosDTO>> createNestedList = null;
            createNestedList = (p) =>
            {
                if (rolePermisos != null)
                    p.Habilitada = rolePermisos.Any(e => e.IdPermiso == p.IdPermiso);

                var childPermissions = permissions.Where(e => e.IdPermisoPadre == p.IdPermiso);
                var childPermissionsDTO = new List<PermisosDTO>(childPermissions.Count());
                if (childPermissions.Any(e => e.IdClase == 5))
                    childPermissionsDTO.Add(new PermisosDTO() { IdPermiso = -1, IdClase = -1, Nombre = "Ingresar al módulo", Habilitada = p.Habilitada });

                foreach (var permission in childPermissions)
                {
                    var permiso = AdaptarPermisoAPermisoDTO(permission);
                    permiso.Permisos = createNestedList(permiso);
                    childPermissionsDTO.Add(permiso);
                }

                return childPermissionsDTO?.ToList();
            };

            permisoDTO.Permisos = createNestedList(permisoDTO);

            return permisoDTO;
        }

        private PermisosDTO AdaptarPermisoAPermisoDTO(TUPermiso permiso)
        {
            return new PermisosDTO()
            {
                IdPermiso = permiso.IdPermiso,
                Nombre = permiso.Nombre,
                IdClase = permiso.IdClase,
                IdEntidadPadre = permiso.IdPermisoPadre
            };
        }

        public IEnumerable<TURolesPermiso> GetRolPermisosDePermisoDTO(string idRol, PermisosDTO permisoDTO)
        {
            var permisos = new List<TURolesPermiso>();
            Action<PermisosDTO> exportElements = null;

            exportElements = (p) =>
            {
                if (p.Habilitada)
                {
                    permisos.Add(new TURolesPermiso()
                    {
                        IdRol = idRol,
                        IdPermiso = p.IdPermiso,
                        Activo = true,
                        EditadoPor = "Admin",
                        UltimaEdicion = DateTime.Now
                    });

                    foreach (var permiso in p.Permisos ?? Enumerable.Empty<PermisosDTO>())
                    {
                        exportElements(permiso);
                    }
                }
            };

            exportElements(permisoDTO);

            return permisos;
        }
    }
}
