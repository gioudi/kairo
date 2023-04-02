using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KAIROSV2.WebApp.Support.ValidationAttributes;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionRolViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }

        [Required(ErrorMessage = "Id rol es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string IdRol { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [NotEqual("IdRol")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [MaxLength(80, ErrorMessage = "La longitud máxima son 80 caracteres")]
        public string Descripcion { get; set; }

        public bool Lectura { get; set; }

        public PermisosDTO Permisos { get; set; }

        public TURole ExtraerRol()
        {
            return new TURole()
            {
                IdRol = IdRol,
                Nombre = Nombre,
                Descripcion = Descripcion,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };
        }


        public void EnableParentsPermissions()
        {
            Func<PermisosDTO, bool> enableParents = null;
            enableParents = (p) =>
            {
                if (p.Permisos != null)
                {
                    foreach (var permission in p.Permisos)
                        if (enableParents(permission))
                            permission.Habilitada = true;

                    var childEnabled = p.Permisos.Any(e => e.Habilitada);
                    p.Permisos?.RemoveAll(e => e.IdPermiso == -1);
                    return childEnabled;
                    
                }
                else
                    return p.Habilitada;
            };

            Permisos.Habilitada = enableParents(Permisos);
        }
    }
}
