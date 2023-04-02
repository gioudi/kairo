using KAIROSV2.WebApp.Identity.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class ActionsPermission
    {
        public ActionsPermission() { }
        public ActionsPermission(System.Security.Claims.ClaimsPrincipal principalClaims, Permissions crear, Permissions borrar, Permissions editar, Permissions detalles, Permissions exportar, Permissions importar)
        {
            var permissionsClaim =
            principalClaims.Claims.SingleOrDefault(c => c.Type == "http://schemas.primax.co/identity/claims/permissions");

            if (permissionsClaim == null)
                return;

            var usersPermissions = permissionsClaim.Value.DecompressPermissionsFromString();
            Crear = usersPermissions.Contains((int)crear);
            Borrar = usersPermissions.Contains((int)borrar);
            Editar = usersPermissions.Contains((int)editar);
            Detalles = usersPermissions.Contains((int)detalles);
            Exportar = usersPermissions.Contains((int)exportar);
            Importar = usersPermissions.Contains((int)importar);
        }

        public bool Crear { get; set; }
        public bool Borrar { get; set; }
        public bool Editar { get; set; }
        public bool Detalles { get; set; }
        public bool Exportar { get; set; }
        public bool Importar { get; set; }
    }
}
