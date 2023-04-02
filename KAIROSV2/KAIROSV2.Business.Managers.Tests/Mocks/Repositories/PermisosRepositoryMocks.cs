using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
    public class PermisosRepositoryMocks
    {
        public static Mock<IPermisosRepository> GetPermissionByRol()
        {
            var permisos =  new List<TUPermiso>()
            {
                  new TUPermiso()
                {
                    IdPermiso = 1,
                    IdPermisoPadre = null
                },
                new TUPermiso()
                {
                    IdPermiso = 2,
                    IdPermisoPadre = 1
                },
                new TUPermiso()
                {
                    IdPermiso = 3,
                    IdPermisoPadre = 1
                },
                new TUPermiso()
                {
                    IdPermiso = 4,
                    IdPermisoPadre = 2
                },
                new TUPermiso()
                {
                    IdPermiso = 5,
                    IdPermisoPadre = 2
                },
                new TUPermiso()
                {
                    IdPermiso = 6,
                    IdPermisoPadre = 3
                },
                new TUPermiso()
                {
                    IdPermiso = 7,
                    IdPermisoPadre = 4
                }
            };

            var mockPermisosRepository = new Mock<IPermisosRepository>();
            mockPermisosRepository.Setup(repo => repo.GetPermissionByRol(It.IsAny<string>())).Returns(permisos);
            return mockPermisosRepository;
        }

        public static Mock<IPermisosRepository> GetPermissionByRolNull()
        {
            List<TUPermiso> permisos = null;

            var mockPermisosRepository = new Mock<IPermisosRepository>();
            mockPermisosRepository.Setup(repo => repo.GetPermissionByRol(It.IsAny<string>())).Returns(permisos);
            return mockPermisosRepository;
        }
    }
}
