using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
    public class DespachosEngineMocks
    {
        public static Mock<IDespachosEngine> CalcularConsolidado()
        {
            List<DespachosConsolidadosDetalleDTO> Despachos = new List<DespachosConsolidadosDetalleDTO>();

            var despacho = new DespachosConsolidadosDetalleDTO()
            {
                Compañia = "",
                DensidadPonderada = 50,
                Factor = 1,
                Fecha = DateTime.Now,
                IdCompañia = "20000001",
                IdProducto = "118323",
                PorcentajePonderado = 0.47,
                Producto = "MOTOR",
                TemperaturaPonderada = 54.98,
                VolumenUnitarioBruto = 3240,
                VolumenUnitarioNeto = 3265


            };

            Despachos.Add(despacho);


            var mockDespachosRepository = new Mock<IDespachosEngine>();
            mockDespachosRepository.Setup(repo => repo.CalcularDespachosConsolidadosDetalle(It.IsAny<string>(), It.IsAny<string>() , It.IsAny<string>() , DateTime.Now )).Returns(Despachos);
            return mockDespachosRepository;
        }
    }
}
