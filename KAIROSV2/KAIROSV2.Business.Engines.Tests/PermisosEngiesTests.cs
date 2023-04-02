using KAIROSV2.Business.Engines.Tests.Modelos;
using KAIROSV2.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace KAIROSV2.Business.Engines.Tests
{
    [TestClass]
    public class PermisosEngiesTests
    {
        private PermisosEngine _SistemaProbado;

        private PermisosEngine SistemaProbado
        {
            get
            {
                if (_SistemaProbado == null)
                {
                    _SistemaProbado =
                        new PermisosEngine();
                }

                return _SistemaProbado;
            }
        }

        [TestMethod]
        public void CreatePermissionHierarchy()
        {
            //Arrange
            var inicial = Permisos.ObtenerPermisoInicial();
            var lista = Permisos.ObtenerListaPermisos();

            //Act
            var result = SistemaProbado.CreatePermissionHierarchy(inicial, lista);

            //Assert
            Assert.IsNotNull(result, "El permiso no deberia ser nulo");
            Assert.IsNotNull(result.InverseIdPermisoPadreNavigation, "La lista de permisos 1 nivel no deberia ser nula");
            Assert.AreEqual(2, result.InverseIdPermisoPadreNavigation.Count, "La lista de permisos 1 nivel deberia tener 2 permisos");
            Assert.AreEqual(2, result.InverseIdPermisoPadreNavigation.First().InverseIdPermisoPadreNavigation.Count, "La lista de permisos 2 nivel deberia tener 2 permisos");
        }

        [TestMethod]
        public void CreatePermissionHierarchy_sin_permiso_inicial()
        {
            //Arrange
            var inicial = Permisos.ObtenerPermisoInicial();
            var lista = Permisos.ObtenerListaPermisos();
            inicial.IdPermiso = 9;

            //Act
            var result = SistemaProbado.CreatePermissionHierarchy(inicial, lista);

            //Assert
            Assert.IsNotNull(result, "El permiso no deberia ser nulo");
            Assert.AreEqual(0, result.InverseIdPermisoPadreNavigation.Count, "La lista de permisos deberia ser nula");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreatePermissionHierarchy_permiso_inicial_nulo()
        {
            //Arrange
            TUPermiso inicial = null;
            var lista = Permisos.ObtenerListaPermisos();

            //Act
            SistemaProbado.CreatePermissionHierarchy(inicial, lista);
        }
    }
}
