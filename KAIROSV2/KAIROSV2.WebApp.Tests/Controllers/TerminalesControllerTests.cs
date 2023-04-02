
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
    public class TerminalesControllerTests
    {
        [TestMethod]
        public void Index()
        {
            //arrange
            var mockTerminalesManager = TerminalesManagerMocks.ObtenerTerminales();
            var mockCompañiasManager = CompañiasManagerMocks.ObtenerCompañias();
            var mockTerminalCompañiasManager = TerminalCompañiasManagerMocks.ObtenerTerminalCompañias();
            var mockAreaManager = AreasManagerMocks.ObtenerAreas();


            var controlador = new Mock<TerminalesController>(mockTerminalesManager.Object, mockCompañiasManager.Object, mockTerminalCompañiasManager.Object, mockAreaManager.Object, null);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.Index();
            var resultModel = ((result as ViewResult)?.Model as ListViewModel<TTerminal>);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "El resultado deberia ser de tipo ViewResult");
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ListViewModel<TTerminal>), "El modelo de la vista deberia se de tipo TerminalesViewModel");
            Assert.AreEqual(1, resultModel.Entidades.Count(), "El modelo deberia tener tres Terminales");
        }

        [TestMethod]
        public void NuevoTerminal()
        {
            //arrange
            var mockTerminalesManager = TerminalesManagerMocks.ObtenerTerminales();
            var mockCompañiasManager = CompañiasManagerMocks.ObtenerCompañias();
            var mockTerminalCompañiasManager = TerminalCompañiasManagerMocks.ObtenerTerminalCompañias();
            var mockAreaManager = AreasManagerMocks.ObtenerAreas();
            var mockProductoManager = ProductosManagerMocks.ObtenerProductosTerminalesRecetas();

            var controlador = new Mock<TerminalesController>(mockTerminalesManager.Object, mockCompañiasManager.Object, mockTerminalCompañiasManager.Object, mockAreaManager.Object, mockProductoManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.NuevaTerminal();
            var resultModel = ((result as PartialViewResult)?.Model as GestionTerminalViewModel);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "El resultado deberia ser de tipo PartialViewResult");
            Assert.AreEqual("_GestionTerminal", (result as PartialViewResult).ViewName, "El nombre de la vista parcial deberia ser _GestionTerminal");
            Assert.IsInstanceOfType((result as PartialViewResult).Model, typeof(GestionTerminalViewModel), "El modelo de la vista deberia se de tipo GestionTerminalViewModel");

        }

        [TestMethod]
        public void DatosTerminal()
        {
            //arrange
            var mockTerminalesManager = TerminalesManagerMocks.ObtenerTerminales();
            var mockCompañiasManager = CompañiasManagerMocks.ObtenerCompañias();
            var mockTerminalCompañiasManager = TerminalCompañiasManagerMocks.ObtenerTerminalCompañias();
            var mockLogManager = LogManagerMocks.ObtenerLog();
            var mockAreaManager = AreasManagerMocks.ObtenerAreas();
            var mockProductoManager = ProductosManagerMocks.ObtenerProductosTerminalesRecetas();

            var datosTerminalPeticion = new DatosTerminalPeticion() { Terminal = "PRIMAX", Lectura = false };
            var controlador = new Mock<TerminalesController>(mockTerminalesManager.Object, mockCompañiasManager.Object, mockTerminalCompañiasManager.Object, mockAreaManager.Object, mockProductoManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.EditarTerminal(datosTerminalPeticion);
            var resultModel = ((result as PartialViewResult)?.Model as GestionTerminalViewModel);
            
            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "El resultado deberia ser de tipo PartialViewResult");
            Assert.AreEqual("_GestionTerminal", (result as PartialViewResult).ViewName, "El nombre de la vista parcial deberia ser _GestionTerminal");
            Assert.IsInstanceOfType((result as PartialViewResult).Model, typeof(GestionTerminalViewModel), "El modelo de la vista deberia se de tipo GestionTerminalViewModel");
            Assert.AreEqual("Actualizar", resultModel.Accion, "La accion deberia ser Actualizar");
        }

        [TestMethod]
        public void DatosTerminal_datoslectura_datosTerminal()
        {
            //arrange
            var mockTerminalesManager = TerminalesManagerMocks.ObtenerTerminales();
            var mockCompañiasManager = CompañiasManagerMocks.ObtenerCompañias();
            var mockTerminalCompañiasManager = TerminalCompañiasManagerMocks.ObtenerTerminalCompañias();
            var mockLogManager = LogManagerMocks.ObtenerLog();
            var mockAreaManager = AreasManagerMocks.ObtenerAreas();
            var mockProductoManager = ProductosManagerMocks.ObtenerProductosTerminalesRecetas();

            var datosTerminalPeticion = new DatosTerminalPeticion() { Terminal = "NORTE", Lectura = true };
            var controlador = new Mock<TerminalesController>(mockTerminalesManager.Object, mockCompañiasManager.Object, mockTerminalCompañiasManager.Object, mockAreaManager.Object, mockProductoManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.EditarTerminal(datosTerminalPeticion);
            var resultModel = ((result as PartialViewResult)?.Model as GestionTerminalViewModel);

            //assert
            Assert.AreEqual("Actualizar", resultModel.Accion, "La accion deberia ser Actualizar");
            Assert.AreEqual("NORTE", resultModel.IdTerminal, "El Id del Terminal deberia ser norte");
        }
    }
}
