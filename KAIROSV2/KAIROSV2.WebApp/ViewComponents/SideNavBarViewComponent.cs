using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewComponents
{
    public class SideNavBarViewComponent : ViewComponent
    {
        private readonly IPermisosManager _permisosManager;
        private readonly UserManager<TUUsuario> _userManager;

        public SideNavBarViewComponent(IPermisosManager permisosManager, UserManager<TUUsuario> userManager)
        {
            _permisosManager = permisosManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var permisos = _permisosManager.GetPermissionsHierarchyByRol(currentUser?.RolId);
            var menus = MapPermissionsToViewModel(permisos?.InverseIdPermisoPadreNavigation);
          
            return View(menus);
        }

        private List<MenuViewModel> MapPermissionsToViewModel(IEnumerable<TUPermiso> permisos)
        {
            List<MenuViewModel> menu = new List<MenuViewModel>(permisos?.Count() ?? 0);

            foreach (var menuItem in permisos?.Where(e => e.IdClase <=4))
            {
                var opcion = new MenuViewModel()
                {
                    IdPermiso = menuItem.IdPermiso,
                    Icono = menuItem.Icono,
                    Nombre = menuItem.Nombre,
                    Controlador = menuItem.Contenido?.Split('/').FirstOrDefault(),
                    Accion = menuItem.Contenido?.Split('/').LastOrDefault(),
                    IdPermisoPadre = menuItem.IdPermisoPadre,
                    Clase = (menuItem.IdClase <= 3 && menuItem.InverseIdPermisoPadreNavigation.Count > 0) ? "collapsible-header" : string.Empty,
                    Color = menuItem.Color,
                };

                opcion.SubMenus = MapPermissionsToViewModel(menuItem.InverseIdPermisoPadreNavigation);
                menu.Add(opcion);
            }

            return menu;
        }
    }
}
