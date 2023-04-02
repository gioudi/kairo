using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class TanquesManagerMocks
    {
        public static Mock<ITanquesManager> ObtenerTanques()
        {
            var Tanques = new List<TTanque>()
            {
                new TTanque
                {
                    IdTanque = "primax",
                    AforadoPor = "John",
                    AlturaMaximaAforo = 100,
                    CapacidadNominal = 80,
                    CapacidadOperativa = 60,
                    ClaseTanque = "Fisico",
                    EditadoPor = "John",
                    FechaAforo = DateTime.Now ,
                    IdEstado = 1,
                    IdTerminal = "Primax",
                    IdProducto = "AcemB100",
                    Observaciones = "",
                    TipoTanque = "Barril",
                    VolumenNoBombeable = 10,
                    FilaId = 1,
                    
                    
                    
                },
                new TTanque
                {
                    IdTanque = "primax1",
                    AforadoPor = "Primax Uno",
                    AlturaMaximaAforo = 80,
                    CapacidadNominal = 90,
                    CapacidadOperativa = 70,
                    ClaseTanque = "",
                    EditadoPor = "",
                    FechaAforo = DateTime.Now,
                    FilaId = 1, 
                    IdEstado = 1, 
                    IdProducto = "",
                    IdTerminal = "Primax",
                    Observaciones = "",
                    PantallaFlotante = true,
                    UltimaEdicion = DateTime.Now,
                    TipoTanque = "",
                    VolumenNoBombeable = 10
                },
                new TTanque
                {
                    IdTanque = "primax1",
                    AforadoPor = "Primax Uno",
                    AlturaMaximaAforo = 80,
                    CapacidadNominal = 90,
                    CapacidadOperativa = 70,
                    ClaseTanque = "",
                    EditadoPor = "",
                    FechaAforo = DateTime.Now,
                    FilaId = 1,
                    IdEstado = 1,
                    IdProducto = "",
                    IdTerminal = "Primax",
                    Observaciones = "",
                    PantallaFlotante = true,
                    UltimaEdicion = DateTime.Now,
                    TipoTanque = "",
                    VolumenNoBombeable = 10
                }
            };

            var mockTanquesManager = new Mock<ITanquesManager>();
            mockTanquesManager.Setup(repo => repo.ObtenerTanques(It.IsAny<string>())).Returns(Tanques);
            return mockTanquesManager;
        }

        public static Mock<ITanquesManager> ObtenerTanque()
        {
            var Tanque = new TTanque()
            {
                IdTanque = "GASOLINA",
                AforadoPor = "Primax Uno",
                AlturaMaximaAforo = 80,
                CapacidadNominal = 90,
                CapacidadOperativa = 70,
                ClaseTanque = "",
                EditadoPor = "",
                FechaAforo = DateTime.Now,
                FilaId = 1,
                IdEstado = 1,
                IdProducto = "",
                IdTerminal = "Primax",
                Observaciones = "",
                PantallaFlotante = true,
                UltimaEdicion = DateTime.Now,
                TipoTanque = "",
                VolumenNoBombeable = 10
            };

            var mockTanquesManager = new Mock<ITanquesManager>();
            mockTanquesManager.Setup(repo => repo.ObtenerTanque(It.IsAny<string>(), It.IsAny<string>())).Returns(Tanque);
            return mockTanquesManager;
        }

    }
}
