using KAIROSV2.Business.Managers.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAIROSV2.Business.Managers;
using KAIROSV2.Business.Entities;
using System;
using System.Linq;
using Moq;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers.Tests
{
    [TestClass]
    public class ProveedoresManagerTests
    {
        [TestMethod]
        public void BorrarProveedor()
        {
            //Arrange
            var mockProveedoresRepository = ProveedoresRepositoryMocks.GetExistAndRemove(true);
            var manager = new Mock<ProveedoresManager>(mockProveedoresRepository.Object, null);
            manager.CallBase = true;
            manager.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));


            //Act
            var result = manager.Object.BorrarProveedor("idProveedor");

            //Assert
            Assert.IsTrue(result, "El resultado deberia ser verdadero");
        }

        [TestMethod]
        public void BorrarProveedor_no_existente()
        {
            //Arrange
            var mockProveedoresRepository = ProveedoresRepositoryMocks.GetExistAndRemove(false);
            var manager = new ProveedoresManager(mockProveedoresRepository.Object, null);

            //Act
            var result = manager.BorrarProveedor("idProveedor");

            //Assert
            Assert.IsFalse(result, "El resultado deberia ser negativo");
        }

    }
}
