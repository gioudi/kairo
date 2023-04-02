
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using KAIROSV2.WebApp.Tests.Mocks;
using KAIROSV2.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using KAIROSV2.WebApp.ViewModels;
using KAIROSV2.WebApp.Models;
using Moq;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Business.Entities.DTOs;

namespace KAIROSV2.WebApp.Tests.Controllers
{
    [TestClass]
    public class TanquesControllerTests
    {
        [TestMethod]
        public void Index()
        {
            //arrange
            var mockTanquesManager = TanquesManagerMocks.ObtenerTanques();
            var mockTerminalessManager = TerminalesManagerMocks.ObtenerTerminales();
            var mockLogManager = LogManagerMocks.ObtenerLog();

            var controlador = new Mock<TanquesController>(mockTanquesManager.Object, mockTerminalessManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.Index();
            var resultModel = ((result as ViewResult)?.Model as ListViewModel<TanqueDTO>);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "El resultado deberia ser de tipo ViewResult");
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ListViewModel<TanqueDTO>), "El modelo de la vista deberia se de tipo TanquesViewModel");
            Assert.AreEqual(3, resultModel.Entidades.Count(), "El modelo deberia tener tres Tanques");
        }

        [TestMethod]
        public void NuevoTanque()
        {
            //arrange
            var mockTanquesManager = TanquesManagerMocks.ObtenerTanques();

            var mockTerminalessManager = TerminalesManagerMocks.ObtenerTerminales();
            var mockLogManager = LogManagerMocks.ObtenerLog();

            var controlador = new Mock<TanquesController>(mockTanquesManager.Object, mockTerminalessManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.NuevoTanque();
            var resultModel = ((result as PartialViewResult)?.Model as GestionTanqueViewModel);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "El resultado deberia ser de tipo PartialViewResult");
            Assert.AreEqual("_GestionTanque", (result as PartialViewResult).ViewName, "El nombre de la vista parcial deberia ser _GestionTanque");
            Assert.IsInstanceOfType((result as PartialViewResult).Model, typeof(GestionTanqueViewModel), "El modelo de la vista deberia se de tipo GestionTanqueViewModel");

        }

        [TestMethod]
        public void EditarTanque()
        {
            //arrange
            var mockTanquesManager = TanquesManagerMocks.ObtenerTanque();
            var mockTerminalessManager = TerminalesManagerMocks.ObtenerTerminales();
            var mockLogManager = LogManagerMocks.ObtenerLog();

            var datosTanquePeticion = new DatosTanquePeticion() { Tanque = "GASOLINA", Lectura = false };

            var controlador = new Mock<TanquesController>(mockTanquesManager.Object, mockTerminalessManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.EditarTanque(datosTanquePeticion);
            var resultModel = ((result as PartialViewResult)?.Model as GestionTanqueViewModel);


            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "El resultado deberia ser de tipo PartialViewResult");
            Assert.AreEqual("_GestionTanque", (result as PartialViewResult).ViewName, "El nombre de la vista parcial deberia ser _GestionTanque");
            Assert.IsInstanceOfType((result as PartialViewResult).Model, typeof(GestionTanqueViewModel), "El modelo de la vista deberia se de tipo GestionTanqueViewModel");
            Assert.AreEqual("Actualizar", resultModel.Accion, "La accion deberia ser Actualizar");
        }


    }
}
