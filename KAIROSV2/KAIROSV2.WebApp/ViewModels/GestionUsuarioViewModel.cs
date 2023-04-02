using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.Support.Util;
using KAIROSV2.WebApp.Support.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionUsuarioViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }

        public byte[] Foto { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(500)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Imagen { get; set; }

        [Required(ErrorMessage = "Id usuario es obligatorio")]
        [MinLength(5, ErrorMessage = "La longitud minima son 5 caracteres")]
        [MaxLength(15, ErrorMessage = "La longitud máxima son 15 caracteres")]
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = "Nombre usuario es obligatorio")]
        [MinLength(5, ErrorMessage = "La longitud minima son 5 caracteres")]
        [MaxLength(80, ErrorMessage = "La longitud máxima son 80 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Rol usuario es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string RolId { get; set; }

        [MinLength(0, ErrorMessage = "La longitud minima es de 0 caracteres")]
        [MaxLength(15, ErrorMessage = "La longitud máxima son 15 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Email usuario es obligatorio")]
        [EmailAddress(ErrorMessage = "No es un email valido")]
        [MinLength(18, ErrorMessage = "La longitud minima son 18 caracteres")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Email { get; set; }

        public bool Lectura { get; set; }

        public List<string> Roles { get; set; }

        public List<TerminalCompañiaDTO> TerminalCompañia { get; set; }

        public void AsignarUsuario(TUUsuario usuario)
        {
            IdUsuario = usuario?.IdUsuario;
            Nombre = usuario?.Nombre;
            RolId = usuario?.RolId;
            Email = usuario?.Email;
            Telefono = usuario?.Telefono.ToString();
            Foto = usuario?.TUUsuarioImagen?.Imagen;
        }

        public TUUsuario ExtraerUsuario()
        {
            var usuario = new TUUsuario()
            {
                IdUsuario = IdUsuario,
                Nombre = Nombre,
                RolId = RolId,
                Email = Email,
                Telefono = Telefono,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };

            if (Imagen?.Length > 0)
            {
                usuario.TUUsuarioImagen = new TUUsuariosImagen()
                {
                    IdUsuario = IdUsuario,
                    EditadoPor = "Admin",
                    UltimaEdicion = DateTime.Now,
                    Imagen = Imagen.ToByteArray()
                };
            }

            return usuario;

        }

    }
}
