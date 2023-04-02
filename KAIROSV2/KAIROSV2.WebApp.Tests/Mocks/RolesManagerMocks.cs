using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class RolesManagerMocks
    {
        public static Mock<IRolesManager> ObtenerRoles()
        {
            var roles = new List<TURole>()
            {
                new TURole
                {
                    IdRol = "Admin"
                },
                new TURole
                {
                    IdRol = "Operador"
                },
                new TURole
                {
                    IdRol = "Reportes"
                }
            };

            var mockRolesManager = new Mock<IRolesManager>();
            mockRolesManager.Setup(repo => repo.ObtenerRoles()).Returns(roles);
            return mockRolesManager;
        }
    }
}
