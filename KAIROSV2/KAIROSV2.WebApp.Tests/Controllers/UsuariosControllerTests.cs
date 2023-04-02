
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using KAIROSV2.WebApp.Tests.Mocks;
using KAIROSV2.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using KAIROSV2.WebApp.ViewModels;
using KAIROSV2.WebApp.Models;
using KAIROSV2.Business.Entities;
using Moq;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.WebApp.Tests.Controllers
{
    [TestClass]
    public class UsuariosControllerTests
    {
        [TestMethod]
        public void Index()
        {
            //arrange
            var mockUsuariosManager = UsuariosManagerMocks.ObtenerUsuarios();

            var controlador = new Mock<UsuariosController>(null,mockUsuariosManager.Object,null, null, null);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.Index();
            var resultModel = ((result as ViewResult)?.Model as ListViewModel<TUUsuario>);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "El resultado deberia ser de tipo ViewResult");
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ListViewModel<TUUsuario>), "El modelo de la vista deberia se de tipo UsuariosViewModel");
            Assert.AreEqual(3, resultModel.Entidades.Count(), "El modelo deberia tener tres usuarios");
        }

        [TestMethod]
        public void NuevoUsuario()
        {
            //arrange
            var mockTerminalesCompañiaManager = TerminalCompañiasManagerMocks.ObtenerBaseJerarquiaTerminalesCompañia();
            var mockRolesManager = RolesManagerMocks.ObtenerRoles();

            var controlador = new Mock<UsuariosController>(null, null,  mockTerminalesCompañiaManager.Object, mockRolesManager.Object, null);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.NuevoUsuario();
            var resultModel = ((result as PartialViewResult)?.Model as GestionUsuarioViewModel);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "El resultado deberia ser de tipo PartialViewResult");
            Assert.AreEqual("_GestionUsuario", (result as PartialViewResult).ViewName, "El nombre de la vista parcial deberia ser _GestionUsuario");
            Assert.IsInstanceOfType((result as PartialViewResult).Model, typeof(GestionUsuarioViewModel), "El modelo de la vista deberia se de tipo GestionUsuarioViewModel");
            Assert.AreEqual(2, resultModel.TerminalCompañia.Count(), "El modelo deberia tener dos terminales");
        }

        [TestMethod]
        public void DatosUsuario()
        {
            //arrange
            var mockUsuariosManager = UsuariosManagerMocks.ObtenerUsuario();
            var mockTerminalesCompañiaManager = TerminalCompañiasManagerMocks.ObtenerBaseJerarquiaTerminalesCompañiaUsuario();
            var mockRolesManager = RolesManagerMocks.ObtenerRoles();
            var datosUsuarioPeticion = new DatosConsultaPeticion() { IdEntidad = "primax", Lectura = false };

            var controlador = new Mock<UsuariosController>(null, mockUsuariosManager.Object, mockTerminalesCompañiaManager.Object, mockRolesManager.Object, null);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.DatosUsuario(datosUsuarioPeticion);
            var resultModel = ((result as PartialViewResult)?.Model as GestionUsuarioViewModel);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "El resultado deberia ser de tipo PartialViewResult");
            Assert.AreEqual("_GestionUsuario", (result as PartialViewResult).ViewName, "El nombre de la vista parcial deberia ser _GestionUsuario");
            Assert.IsInstanceOfType((result as PartialViewResult).Model, typeof(GestionUsuarioViewModel), "El modelo de la vista deberia se de tipo GestionUsuarioViewModel");
            Assert.AreEqual(1, resultModel.TerminalCompañia.Count(e => e.Habilitada), "El modelo deberia tener una terminal habilitada");
            Assert.AreEqual(2, resultModel.TerminalCompañia.First().Compañias.Count(e => e.Habilitada), "El modelo deberia tener dos compañias habilitadas");
            Assert.AreEqual("Actualizar", resultModel.Accion, "La accion deberia ser Actualizar");
        }

        [TestMethod]
        public void DatosUsuario_datoslectura_datosusuario()
        {
            //arrange
            var mockUsuariosManager = UsuariosManagerMocks.ObtenerUsuario();
            var mockTerminalesCompañiaManager = TerminalCompañiasManagerMocks.ObtenerBaseJerarquiaTerminalesCompañiaUsuario();
            var mockRolesManager = RolesManagerMocks.ObtenerRoles();
            var datosUsuarioPeticion = new DatosConsultaPeticion() { IdEntidad = "primax", Lectura = true };

            var controlador = new Mock<UsuariosController>(null, mockUsuariosManager.Object, mockTerminalesCompañiaManager.Object, mockRolesManager.Object, null);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.DatosUsuario(datosUsuarioPeticion);
            var resultModel = ((result as PartialViewResult)?.Model as GestionUsuarioViewModel);

            //assert
            Assert.AreEqual("", resultModel.Accion, "La accion deberia estar vacia");
            Assert.AreEqual("primax1", resultModel.IdUsuario, "El Id del usuario deberia ser primax1");
        }
    }
}
