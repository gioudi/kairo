using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class AreasManagerMocks
    {
        public static Mock<IAreasManager> ObtenerAreas()
        {
            //ObtenerBaseJerarquiaArea
            var Area = new List<TArea>()
            {
                new TArea
                {
                    Area = "1",
                    IdArea = "SUR"
                }
            };

            var mockAreasManager = new Mock<IAreasManager>();
            mockAreasManager.Setup(repo => repo.ObtenerAreas()).Returns(Area);
            return mockAreasManager;
        }

       
    }
}
