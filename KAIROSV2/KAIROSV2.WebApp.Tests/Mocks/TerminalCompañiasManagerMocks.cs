using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class TerminalCompañiasManagerMocks
    {
        public static Mock<ITerminalesCompañiasManager> ObtenerBaseJerarquiaTerminalesCompañia()
        {
            //ObtenerBaseJerarquiaTerminalesCompañia
            var terminalesCompañia = new List<TerminalCompañiaDTO>()
            {
                new TerminalCompañiaDTO
                {
                    IdEntidad = "1",
                    Nombre = "Terminal 1",
                    EsTerminal = true,
                    Compañias = new List<TerminalCompañiaDTO>()
                    {
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "1",
                            Nombre = "Compañia 1",
                            IdEntidadPadre = "1"
                        },
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "2",
                            Nombre = "Compañia 2",
                            IdEntidadPadre = "1"
                        },
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "3",
                            Nombre = "Compañia 3",
                            IdEntidadPadre = "1"
                        }
                    }
                },
                new TerminalCompañiaDTO
                {
                    IdEntidad = "2",
                    Nombre = "Terminal 2",
                    EsTerminal = true,
                    Compañias = new List<TerminalCompañiaDTO>()
                    {
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "1",
                            Nombre = "Compañia 1",
                            IdEntidadPadre = "2"
                        },
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "3",
                            Nombre = "Compañia 3",
                            IdEntidadPadre = "2"
                        }
                    }
                },
            };

            var mockTerminalesCompañiasManager = new Mock<ITerminalesCompañiasManager>();
            mockTerminalesCompañiasManager.Setup(repo => repo.ObtenerBaseJerarquiaTerminalesCompañia()).Returns(terminalesCompañia);
            return mockTerminalesCompañiasManager;
        }

        public static Mock<ITerminalesCompañiasManager> ObtenerBaseJerarquiaTerminalesCompañiaUsuario()
        {
            //ObtenerBaseJerarquiaTerminalesCompañia
            var terminalesCompañia = new List<TerminalCompañiaDTO>()
            {
                new TerminalCompañiaDTO
                {
                    IdEntidad = "1",
                    Nombre = "Terminal 1",
                    EsTerminal = true,
                    Habilitada = true,
                    Compañias = new List<TerminalCompañiaDTO>()
                    {
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "1",
                            Nombre = "Compañia 1",
                            IdEntidadPadre = "1",
                            Habilitada = true
                        },
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "2",
                            Nombre = "Compañia 2",
                            IdEntidadPadre = "1",
                            Habilitada = true
                        },
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "3",
                            Nombre = "Compañia 3",
                            IdEntidadPadre = "1"
                        }
                    }
                },
                new TerminalCompañiaDTO
                {
                    IdEntidad = "2",
                    Nombre = "Terminal 2",
                    EsTerminal = true,
                    Compañias = new List<TerminalCompañiaDTO>()
                    {
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "1",
                            Nombre = "Compañia 1",
                            IdEntidadPadre = "2"
                        },
                        new TerminalCompañiaDTO
                        {
                            IdEntidad = "3",
                            Nombre = "Compañia 3",
                            IdEntidadPadre = "2"
                        }
                    }
                },
            };

            var mockTerminalesCompañiasManager = new Mock<ITerminalesCompañiasManager>();
            mockTerminalesCompañiasManager.Setup(repo => repo.ObtenerBaseJerarquiaTerminalesCompañiaUsuario(It.IsAny<string>())).Returns(terminalesCompañia);
            return mockTerminalesCompañiasManager;
        }
        public static Mock<ITerminalesCompañiasManager> ObtenerTerminalCompañias()
        {
            //ObtenerBaseJerarquiaCompañia
            var Compañia = new List<TTerminalCompañia>()
            {
                new TTerminalCompañia
                {
                    IdCompañia = "1",
                    IdTerminal = "2",
                    Estado = "Activo",
                    Socia = true,
                    PorcentajePropiedad = 80,

                }
            };

            var mockCompañiasManager = new Mock<ITerminalesCompañiasManager>();
            mockCompañiasManager.Setup(repo => repo.ObtenerTerminalCompañias()).Returns(Compañia);
            return mockCompañiasManager;
        }
    }
}
