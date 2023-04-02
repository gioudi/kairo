using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class ContadoresManagerMocks
    {
        public static Mock<IContadoresManager> ObtenerContadores()
        {
            //ObtenerBaseJerarquiaContadores
            var Contadores = new List<TContador>()
            {
                new TContador
                {
                    IdContador = "4T5R",
                    EditadoPor = "John",                    
                    UltimaEdicion = DateTime.Now,
                    

                },

                
                
            };            

            var mockContadoresManager = new Mock<IContadoresManager>();            
            mockContadoresManager.Setup(repo => repo.ObtenerContadores()).Returns(Contadores);
            return mockContadoresManager;
        }

        
    }
}
