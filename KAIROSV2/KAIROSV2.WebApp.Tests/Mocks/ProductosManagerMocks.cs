using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class ProductosManagerMocks
    {
        public static Mock<IProductosManager> ObtenerProductosTerminalesRecetas()
        {
            //ObtenerBaseJerarquiaArea
            var Productos = new List<ProductoTerminalDto>()
            {
                new ProductoTerminalDto
                {
                  Asignado = false,
                  CodigoProducto = "ProductId01",
                  NombreCorto = "Producto01",
                  NombreTerminal = "",
                  Icon = "",
                  Recetas = null,
                }
            };

            var mockProductosManager = new Mock<IProductosManager>();
            mockProductosManager.Setup(repo => repo.ObtenerProductosTerminalesRecetas("")).Returns(Productos);
            return mockProductosManager;
        }

       
    }
}
