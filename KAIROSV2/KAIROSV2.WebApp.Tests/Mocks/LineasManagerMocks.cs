using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    class LineasManagerMocks
    {
        public static Mock<ILineasManager> ObtenerLineas()
        {
            //ObtenerBaseJerarquiaArea
            var Linea = new List<TLinea>()
            {
                new TLinea
                {
                    IdLinea = "1",
                    IdTerminal = "SUR",


                }
            };

            var mockLineasManager = new Mock<ILineasManager>();
            mockLineasManager.Setup(repo => repo.ObtenerLineas()).Returns(Linea);
            return mockLineasManager;
        }
    }
}
