using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class UsuariosManagerMocks
    {
        public static Mock<IUsuariosManager> ObtenerUsuarios()
        {
            var usuarios = new List<TUUsuario>()
            {
                new TUUsuario
                {
                    IdUsuario = "primax",
                    Nombre = "Primax",
                    RolId = "Admin",
                    Email = "primax@primax.com",
                    Telefono = "311 598 1136"
                },
                new TUUsuario
                {
                    IdUsuario = "primax1",
                    Nombre = "Primax Uno",
                    RolId = "Admin",
                    Email = "primax1@primax.com",
                    Telefono = "311 598 1136"
                },
                new TUUsuario
                {
                    IdUsuario = "primax2",
                    Nombre = "Primax Dos",
                    RolId = "Admin",
                    Email = "primax2@primax.com",
                    Telefono = "311 598 1136"
                }
            };

            var mockUsuariosManager = new Mock<IUsuariosManager>();
            mockUsuariosManager.Setup(repo => repo.ObtenerUsuarios()).Returns(usuarios);
            return mockUsuariosManager;
        }

        public static Mock<IUsuariosManager> ObtenerUsuario()
        {
            var usuario = new TUUsuario()
            {
                IdUsuario = "primax1",
                Nombre = "Primax Uno",
                RolId = "Admin",
                Email = "primax1@primax.com",
                Telefono = "311 598 1136"
            };

            var mockUsuariosManager = new Mock<IUsuariosManager>();
            mockUsuariosManager.Setup(repo => repo.ObtenerUsuario(It.IsAny<string>())).Returns(usuario);
            return mockUsuariosManager;
        }

    }
}
