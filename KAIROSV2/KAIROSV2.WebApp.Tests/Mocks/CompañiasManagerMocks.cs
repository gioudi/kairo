using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class CompañiasManagerMocks
    {
        public static Mock<ICompañiasManager> ObtenerCompañias()
        {
            //ObtenerBaseJerarquiaCompañia
            var Compañia = new List<TCompañia>()
            {
                new TCompañia
                {
                    IdCompañia = "1",
                    Nombre = "Terminal 1",
                    CodigoSicom = "4444",
                    DistributionChannel = "66666",
                    Division = "432",
                    SalesOrganization = "66666",
                    SupplierType = "55555"

                }
            };

            var mockCompañiasManager = new Mock<ICompañiasManager>();
            mockCompañiasManager.Setup(repo => repo.ObtenerCompañias()).Returns(Compañia);
            return mockCompañiasManager;
        }

       
    }
}
