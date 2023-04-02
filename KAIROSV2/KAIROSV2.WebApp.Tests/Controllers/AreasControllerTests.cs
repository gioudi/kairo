
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
    public class AreasControllerTests
    {
        [TestMethod]
        public void Index()
        {
            //arrange
            var mockAreasManager = AreasManagerMocks.ObtenerAreas();

            var controlador = new Mock<AreasController>(mockAreasManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.Index();
            var resultModel = ((result as ViewResult)?.Model as ListViewModel<TArea>);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "El resultado deberia ser de tipo ViewResult");
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ListViewModel<TArea>), "El modelo de la vista deberia se de tipo AreasViewModel");
            Assert.AreEqual(1, resultModel.Entidades.Count(), "El modelo deberia tener un Area");
        }

        [TestMethod]
        public void NuevoArea()
        {
            //arrange
            var mockAreasManager = AreasManagerMocks.ObtenerAreas();
            var controlador = new Mock<AreasController>( mockAreasManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.NuevaArea();
            var resultModel = ((result as PartialViewResult)?.Model as GestionAreaViewModel);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "El resultado deberia ser de tipo PartialViewResult");
            Assert.AreEqual("_GestionArea", (result as PartialViewResult).ViewName, "El nombre de la vista parcial deberia ser _GestionArea");
            Assert.IsInstanceOfType((result as PartialViewResult).Model, typeof(GestionAreaViewModel), "El modelo de la vista deberia se de tipo GestionAreaViewModel");
            
        }

        

    }
}
