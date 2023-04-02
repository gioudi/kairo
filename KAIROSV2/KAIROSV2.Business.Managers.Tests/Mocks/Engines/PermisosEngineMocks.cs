using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
    public class PermisosEngineMocks
    {
        public static Mock<IPermisosEngine> CreatePermissionHierarchy()
        {
            var permiso = new TUPermiso()
            {
                IdPermiso = 1,
                IdPermisoPadre = null,
                InverseIdPermisoPadreNavigation = new List<TUPermiso>()
                {
                    new TUPermiso()
                    {
                        IdPermiso = 2,
                        IdPermisoPadre = 1,
                        InverseIdPermisoPadreNavigation = new List<TUPermiso>()
                        {
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
                        }
                    },
                    new TUPermiso()
                    {
                        IdPermiso = 3,
                        IdPermisoPadre = 1,
                        InverseIdPermisoPadreNavigation = new List<TUPermiso>()
                        {
                            new TUPermiso()
                            {
                                IdPermiso = 6,
                                IdPermisoPadre = 3
                            },
                            new TUPermiso()
                            {
                                IdPermiso = 7,
                                IdPermisoPadre = 3
                            }
                        }
                    }
                }
            };

            var mockPermisosRepository = new Mock<IPermisosEngine>();
            mockPermisosRepository.Setup(repo => repo.CreatePermissionHierarchy(It.IsAny<TUPermiso>(), It.IsAny<IEnumerable<TUPermiso>>())).Returns(permiso);
            return mockPermisosRepository;
        }
    }
}
