using KAIROSV2.Business.Managers.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAIROSV2.Business.Managers;
using KAIROSV2.Business.Entities;
using System;

namespace KAIROSV2.Business.Managers.Tests
{
    [TestClass]
    public class PermisosManagerTests
    {
        [TestMethod]
        public void GetPermissionsHierarchyByRol()
        {
            //Arrange
            var mockPermisosRepository = PermisosRepositoryMocks.GetPermissionByRol();
            var mockPermisosEngine = PermisosEngineMocks.CreatePermissionHierarchy();

            var manager = new PermisosManager(mockPermisosRepository.Object, mockPermisosEngine.Object,null, null);

            //Act
            var result = manager.GetPermissionsHierarchyByRol("Admin");

            //Assert
            Assert.IsNotNull(result, "No deberia ser nulo el resultado");
            Assert.IsInstanceOfType(result, typeof(TUPermiso), "El tipo devuelto deberia ser TUPermiso");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Deberia lanzar una excepcion por argumento nulo")]
        public void GetPermissionsHierarchyByRol_rol_no_encontrado()
        {
            //Arrange
            var mockPermisosRepository = PermisosRepositoryMocks.GetPermissionByRolNull();
            var mockPermisosEngine = PermisosEngineMocks.CreatePermissionHierarchy();

            var manager = new PermisosManager(mockPermisosRepository.Object, mockPermisosEngine.Object, null, null);

            //Act
            manager.GetPermissionsHierarchyByRol("Admin");
        }
    }
}
