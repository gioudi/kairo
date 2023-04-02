using KAIROSV2.Business.Managers.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAIROSV2.Business.Managers;
using KAIROSV2.Business.Entities;
using System;
using Moq;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers.Tests
{
    [TestClass]
    public class UsuariosManagerTests
    {
        [TestMethod]
        public void CrearUsuario()
        {
            //Arrange
            var mockUsuariosRepository = UsuariosRepositoryMocks.GetAddUpdateExists(false);
            var manager = new Mock<UsuariosManager>(mockUsuariosRepository.Object, null);
            manager.CallBase = true;
            manager.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //Act
            var result = manager.Object.CrearUsuario(new TUUsuario() { IdUsuario = "primax"});

            //Assert
            Assert.IsTrue(result, "El resultado deberia ser verdadero");
        }

        [TestMethod]
        public void CrearUsuario_Existente()
        {
            //Arrange
            var mockUsuariosRepository = UsuariosRepositoryMocks.GetAddUpdateExists(true);
            var manager = new UsuariosManager(mockUsuariosRepository.Object, null);

            //Act
            var result = manager.CrearUsuario(new TUUsuario() { IdUsuario = "primax" });

            //Assert
            Assert.IsFalse(result, "El resultado deberia ser negativo");
        }

        [TestMethod]
        public void ActualizarUsuario()
        {
            //Arrange
            var mockUsuariosRepository = UsuariosRepositoryMocks.GetAddUpdateExists(true);
            var manager = new Mock<UsuariosManager>(mockUsuariosRepository.Object, null);
            manager.CallBase = true;
            manager.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //Act
            var result = manager.Object.ActualizarUsuario(new TUUsuario() { IdUsuario = "primax" });

            //Assert
            Assert.IsTrue(result, "El resultado deberia ser verdadero");
        }

        [TestMethod]
        public void ActualizarUsuario_no_existente()
        {
            //Arrange
            var mockUsuariosRepository = UsuariosRepositoryMocks.GetAddUpdateExists(false);
            var manager = new UsuariosManager(mockUsuariosRepository.Object, null);

            //Act
            var result = manager.ActualizarUsuario(new TUUsuario() { IdUsuario = "primax" });

            //Assert
            Assert.IsFalse(result, "El resultado deberia ser negativo");
        }
    }
}
