using KAIROSV2.Business.Managers.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAIROSV2.Business.Managers;
using KAIROSV2.Business.Entities;
using System;
using System.Linq;
using KAIROSV2.WebApp.Tests.Mocks;
using System.Collections.Generic;

namespace KAIROSV2.Business.Managers.Tests
{
    [TestClass]
    public class DespachosManagerTests
    {
        [TestMethod]
        public void BorrarProveedor()
        {
            //Arrange
            var mockDespachosRepository = DespachosRepositoryMocks.GetExistAndRemove(true);
            var mockLogManager = LogManagerMocks.ObtenerLog();
            var mocKDespachosEngine = DespachosEngineMocks.CalcularConsolidado();
            var mockUsuariosTerminalCompañiaRepository = UsuariosTerminalCompañiaRepository.GetUsuarioTerminalCompañia();
                        

            //Assert
            
        }

        

    }
}
