
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
using KAIROSV2.Business.Entities;

namespace KAIROSV2.WebApp.Tests.Controllers
{
    [TestClass]
    public class ContadoresControllerTests
    {
        [TestMethod]
        public void Index()
        {
            //arrange
            var mockContadoresManager = ContadoresManagerMocks.ObtenerContadores();            
            var mockLogManager = LogManagerMocks.ObtenerLog();
            
            var controlador = new Mock<ContadoresController>(mockContadoresManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.Index();
            var resultModel = ((result as ViewResult)?.Model as ListViewModel<TContador>);

            //assert
            Assert.IsNotNull(result, "La vista no debería ser nula");
            Assert.IsInstanceOfType(result, typeof(ViewResult), "El resultado debería ser de tipo ViewResult");
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ListViewModel<TContador>), "El modelo de la vista debería se de tipo ContadoresViewModel");
            Assert.AreEqual(1, resultModel.Entidades.Count() , "El modelo debería tener tres Contadores");
        }

        [TestMethod]
        public void NuevoContador()
        {
            //arrange
            var mockContadoresManager = ContadoresManagerMocks.ObtenerContadores();                    
            var mockLogManager = LogManagerMocks.ObtenerLog();
            
            var controlador = new Mock<ContadoresController>(mockContadoresManager.Object);
            controlador.CallBase = true;
            controlador.Setup(t => t.LogInformacion(It.IsAny<LogAcciones>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var result = controlador.Object.NuevoContador();
            var resultModel = ((result as PartialViewResult)?.Model as GestionContadorViewModel);

            //assert
            Assert.IsNotNull(result, "La vista no deberia ser nula");
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "El resultado deberia ser de tipo PartialViewResult");
            Assert.AreEqual("_GestionContador", (result as PartialViewResult).ViewName, "El nombre de la vista parcial deberia ser _GestionContador");
            Assert.IsInstanceOfType((result as PartialViewResult).Model, typeof(GestionContadorViewModel), "El modelo de la vista deberia se de tipo GestionContadorViewModel");
            
        }
      
    }
}
