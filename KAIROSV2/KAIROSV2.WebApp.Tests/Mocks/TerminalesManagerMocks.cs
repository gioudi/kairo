using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class TerminalesManagerMocks
    {
        public static Mock<ITerminalesManager> ObtenerTerminales()
        {
            //ObtenerBaseJerarquiaTerminales
            var Terminales = new List<TTerminal>()
            {
                new TTerminal
                {
                    IdTerminal = "4T5R",
                    IdArea = "Terminal 1",
                    CentroCosto = "Bogota",
                    IdEstado = 1,
                    Conjunta = false,
                    Direccion = "Bogota",
                    Poliducto = "Bogota",
                    EditadoPor = "Admin",
                    IdCompañiaOperadora = "Bogota",
                    Superintendente = "John",
                    Telefono = "33333333",
                    Terminal = "Bogota" ,
                    VentasTerceros = true
                  
                },
                
            };

            var terminalEstados = new List<TTerminalesEstado>()
            {
                new TTerminalesEstado
                {
                    IdEstado = 1,
                    Descripcion = "Activa",
                    
                }
                
            };

            var mockTerminalesManager = new Mock<ITerminalesManager>();
            mockTerminalesManager.Setup(repo => repo.ObtenerEstadosTerminal()).Returns(terminalEstados);
            mockTerminalesManager.Setup(repo => repo.ObtenerTerminales(It.IsAny<string[]>())).Returns(Terminales);
            return mockTerminalesManager;
        }

        
    }
}
