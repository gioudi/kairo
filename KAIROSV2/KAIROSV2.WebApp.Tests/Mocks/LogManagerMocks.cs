using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.WebApp.Tests.Mocks
{
    public class LogManagerMocks
    {
        public static Mock<ILogManager> ObtenerLog()
        {
            //ObtenerBaseJerarquiaLog
            var Log = new List<TLog>()
            {
                new TLog
                {
                    Id = 1,
                    Accion = 1,
                    Aplicacion = "Kairos2",
                    Area = "Terminales",
                    Comentario = "",
                    Entidad = "T_Terminales",
                    FechaEvento = DateTime.Now,
                    IdUsuario = "Admin",
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = "Terminales"
                }
            };

            var mockLogsManager = new Mock<ILogManager>();
            mockLogsManager.Setup(repo => repo.ObtenerDatosPorFechas(DateTime.Now , DateTime.Now )).Returns(Log);
            return mockLogsManager;
        }

       
    }
}
