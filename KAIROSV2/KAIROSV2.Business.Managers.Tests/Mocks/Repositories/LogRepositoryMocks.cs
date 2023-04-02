using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
   public class LogRepositoryMocks
    {
        public static Mock<ILogRepository> ObtenerLogs()
        {
            var Log = new List<TLog>()
            {
                new TLog()
                {
                    IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""


                },
                new TLog()
                {
                    IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""
                },
                 new TLog()
                {
                     IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""
                },
                 new TLog()
                {
                     IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""
                },
                 new TLog()
                {
                     IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""
                }
            };

            var mockLogRepository = new Mock<ILogRepository>();
            mockLogRepository.Setup(repo => repo.GetLogsByDates(DateTime.Now, DateTime.Now)).Returns(Log);
            return mockLogRepository;
        }


        public static Mock<ILogRepository> GetUsuarioLog()
        {
            var Log = new List<TLog>()
            {
                new TLog()
                {
                    IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""


                },
                new TLog()
                {
                    IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""
                },
                 new TLog()
                {
                     IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""
                },
                 new TLog()
                {
                     IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""
                },
                 new TLog()
                {
                     IdUsuario = "primax",
                    Accion = 1,
                    Aplicacion = "1",
                    Area = "Bogota",
                    Comentario = "",
                    Entidad = "",
                    FechaEvento = DateTime.Now,
                    Id = 1,
                    Objetivo = "",
                    Prioridad = 1,
                    Seccion = ""
                }
            };

            var mockLogRepository = new Mock<ILogRepository>();
            mockLogRepository.Setup(repo => repo.GetLogsByDates( DateTime.Now , DateTime.Now )).Returns(Log);
            return mockLogRepository;
        }
    }
}
