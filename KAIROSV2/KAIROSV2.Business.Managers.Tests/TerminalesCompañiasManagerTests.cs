
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAIROSV2.Business.Managers;
using KAIROSV2.Business.Entities;
using System;
using System.Linq;
using KAIROSV2.Business.Managers.Tests.Mocks;
using KAIROSV2.WebApp.Tests.Mocks;

namespace KAIROSV2.Business.Managers.Tests
{
    [TestClass]
    public class TerminalesCompañiasManagerTests
    {
        [TestMethod]
        public void ObtenerBaseJerarquiaTerminalesCompañiaUsuario()
        {
            //Arrange
            var mockTerminalCompañiaRepository = TerminalCompañiaRepositoryMocks.GetTerminalCompañia();
            var mockUsuariosTerminalCompañiaRepository = UsuariosTerminalCompañiaRepository.GetUsuarioTerminalCompañia();
            var mockLogManager = LogManagerMocks.ObtenerLog();

            var manager = new TerminalesCompañiasManager(mockTerminalCompañiaRepository.Object, mockUsuariosTerminalCompañiaRepository.Object, null);

            //Act
            var result = manager.ObtenerBaseJerarquiaTerminalesCompañiaUsuario("primax");

            //Assert
            Assert.IsNotNull(result, "No deberia ser nulo el resultado");
            Assert.AreEqual(2, result.Count(e => e.EsTerminal && e.Habilitada), "Deberian haber dos terminales habilitadas");
            Assert.AreEqual(2, result.First().Compañias.Count(e => !e.EsTerminal && e.Habilitada), "Deberian haber dos compañias habilitadas para terminal 1");
            Assert.AreEqual(1, result.First(t => t.IdEntidad == "2").Compañias.Count(e => !e.EsTerminal && e.Habilitada), "Deberian haber una compañia habilitada para terminal 2");
        }

        [TestMethod]
        public void ObtenerBaseJerarquiaTerminalesCompañiaUsuario_jerarquia_bien()
        {
            //Arrange
            var mockTerminalCompañiaRepository = TerminalCompañiaRepositoryMocks.GetTerminalCompañia();
            var mockUsuariosTerminalCompañiaRepository = UsuariosTerminalCompañiaRepository.GetUsuarioTerminalCompañiaDatos2();
            var mockLogManager = LogManagerMocks.ObtenerLog();

            var manager = new TerminalesCompañiasManager(mockTerminalCompañiaRepository.Object, mockUsuariosTerminalCompañiaRepository.Object , null);

            //Act
            var result = manager.ObtenerBaseJerarquiaTerminalesCompañiaUsuario("primax");

            //Assert
            Assert.IsNotNull(result, "No deberia ser nulo el resultado");
            Assert.AreEqual(3, result.Count(), "Deberian haber 3 terminales");
            Assert.AreEqual(2, result.First().Compañias.Count, "Deberian haber 2 compañias para terminal 1");
            Assert.AreEqual(3, result.First(e => e.IdEntidad == "2").Compañias.Count, "Deberian haber 3 compañias para terminal 2");
            Assert.AreEqual(2, result.First(e => e.IdEntidad == "3").Compañias.Count, "Deberian haber 2 compañias para terminal 3");
        }

        [TestMethod]
        public void ObtenerBaseJerarquiaTerminalesCompañiaUsuario_no_habilitadas()
        {
            //Arrange
            var mockTerminalCompañiaRepository = TerminalCompañiaRepositoryMocks.GetTerminalCompañia();
            var mockUsuariosTerminalCompañiaRepository = UsuariosTerminalCompañiaRepository.GetUsuarioTerminalCompañiaDatos2();
            var mockLogManager = LogManagerMocks.ObtenerLog();

            var manager = new TerminalesCompañiasManager(mockTerminalCompañiaRepository.Object, mockUsuariosTerminalCompañiaRepository.Object , null);

            //Act
            var result = manager.ObtenerBaseJerarquiaTerminalesCompañiaUsuario("primax");

            //Assert
            Assert.IsNotNull(result, "No deberia ser nulo el resultado");
            Assert.AreEqual(3, result.Count(), "Deberian haber 3 terminales");
            Assert.AreEqual(2, result.First().Compañias.Count, "Deberian haber 2 compañias para terminal 1");
            Assert.AreEqual(3, result.First(e => e.IdEntidad == "2").Compañias.Count, "Deberian haber 3 compañias para terminal 2");
            Assert.AreEqual(2, result.First(e => e.IdEntidad == "3").Compañias.Count, "Deberian haber 2 compañias para terminal 3");
            Assert.AreEqual(0, result.Count(e => e.EsTerminal && e.Habilitada), "No deberian haber terminales habilitadas");
            Assert.AreEqual(0, result.First().Compañias.Count(e => !e.EsTerminal && e.Habilitada), "No Deberian haber compañias habilitadas para terminal 1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Se esperaba una excepcion por argumento nulo")]
        public void ObtenerBaseJerarquiaTerminalesCompañiaUsuario_usuario_sin_terminales_compañias()
        {
            //Arrange
            var mockTerminalCompañiaRepository = TerminalCompañiaRepositoryMocks.GetTerminalCompañia();
            var mockUsuariosTerminalCompañiaRepository = UsuariosTerminalCompañiaRepository.GetUsuarioTerminalCompañiaDatosNulos();
            var LogManagerMock = LogManagerMocks.ObtenerLog();

            var manager = new TerminalesCompañiasManager(mockTerminalCompañiaRepository.Object, mockUsuariosTerminalCompañiaRepository.Object , null);

            //Act
            manager.ObtenerBaseJerarquiaTerminalesCompañiaUsuario("primax");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Se esperaba una excepcion por argumento nulo")]
        public void ObtenerBaseJerarquiaTerminalesCompañiaUsuario_sin_terminales_compañias()
        {
            //Arrange
            var mockTerminalCompañiaRepository = TerminalCompañiaRepositoryMocks.GetTerminalCompañiaNulo();
            var mockUsuariosTerminalCompañiaRepository = UsuariosTerminalCompañiaRepository.GetUsuarioTerminalCompañiaDatosNulos();
            var LogManagerMock = LogManagerMocks.ObtenerLog();

            var manager = new TerminalesCompañiasManager(mockTerminalCompañiaRepository.Object, mockUsuariosTerminalCompañiaRepository.Object, null);

            //Act
            manager.ObtenerBaseJerarquiaTerminalesCompañiaUsuario("primax");
        }

        [TestMethod]
        public void ObtenerBaseJerarquiaTerminalesCompañia()
        {
            //Arrange
            var mockTerminalCompañiaRepository = TerminalCompañiaRepositoryMocks.GetTerminalCompañia();
            var mockUsuariosTerminalCompañiaRepository = UsuariosTerminalCompañiaRepository.GetUsuarioTerminalCompañiaDatos2(); 
            var LogManagerMock = LogManagerMocks.ObtenerLog();
            

            var manager = new TerminalesCompañiasManager(mockTerminalCompañiaRepository.Object, mockUsuariosTerminalCompañiaRepository.Object, null);


            //Act
            var result = manager.ObtenerBaseJerarquiaTerminalesCompañia();

            //Assert
            Assert.IsNotNull(result, "No deberia ser nulo el resultado");
            Assert.AreEqual(3, result.Count(), "Deberian haber 3 terminales");
            Assert.AreEqual(2, result.First().Compañias.Count, "Deberian haber 2 compañias para terminal 1");
            Assert.AreEqual(3, result.First(e => e.IdEntidad == "2").Compañias.Count, "Deberian haber 3 compañias para terminal 2");
            Assert.AreEqual(2, result.First(e => e.IdEntidad == "3").Compañias.Count, "Deberian haber 2 compañias para terminal 3");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Se esperaba una excepcion por argumento nulo")]
        public void ObtenerBaseJerarquiaTerminalesCompañia_sin_terminales_compañias()
        {
            //Arrange
            var mockTerminalCompañiaRepository = TerminalCompañiaRepositoryMocks.GetTerminalCompañiaNulo();
            var LogManagerMock = LogManagerMocks.ObtenerLog();

            var manager = new TerminalesCompañiasManager(mockTerminalCompañiaRepository.Object, null, null);
            
            //Act
            manager.ObtenerBaseJerarquiaTerminalesCompañia();
        }
    }
}
