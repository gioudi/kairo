using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
    public class UsuariosTerminalCompañiaRepository
    {
        public static Mock<IUsuariosTerminalCompañiaRepository> GetUsuarioTerminalCompañia()
        {
            var terminalesCompañia = new List<TUUsuariosTerminalCompañia>()
            {
                new TUUsuariosTerminalCompañia()
                {
                    IdUsuario = "primax",
                    IdTerminal = "1",
                    IdCompañia = "1"
                },
                new TUUsuariosTerminalCompañia()
                {
                    IdUsuario = "primax",
                    IdTerminal = "1",
                    IdCompañia = "2"
                },
                 new TUUsuariosTerminalCompañia()
                {
                     IdUsuario = "primax",
                    IdTerminal = "2",
                    IdCompañia = "1"
                },
                 new TUUsuariosTerminalCompañia()
                {
                     IdUsuario = "primax",
                    IdTerminal = "2",
                    IdCompañia = "6"
                },
                 new TUUsuariosTerminalCompañia()
                {
                     IdUsuario = "primax",
                    IdTerminal = "5",
                    IdCompañia = "3"
                }
            };

            var mockTerminalCompañiaRepository = new Mock<IUsuariosTerminalCompañiaRepository>();
            mockTerminalCompañiaRepository.Setup(repo => repo.Get(It.IsAny<string>())).Returns(terminalesCompañia);
            return mockTerminalCompañiaRepository;
        }
        public static Mock<IUsuariosTerminalCompañiaRepository> GetUsuarioTerminalCompañiaDatos2()
        {
            var terminalesCompañia = new List<TUUsuariosTerminalCompañia>()
            {
                new TUUsuariosTerminalCompañia()
                {
                    IdUsuario = "primax",
                    IdTerminal = "5",
                    IdCompañia = "1"
                },
                new TUUsuariosTerminalCompañia()
                {
                    IdUsuario = "primax",
                    IdTerminal = "5",
                    IdCompañia = "2"
                },
                 new TUUsuariosTerminalCompañia()
                {
                     IdUsuario = "primax",
                    IdTerminal = "6",
                    IdCompañia = "1"
                },
                 new TUUsuariosTerminalCompañia()
                {
                     IdUsuario = "primax",
                    IdTerminal = "6",
                    IdCompañia = "3"
                },
                 new TUUsuariosTerminalCompañia()
                {
                     IdUsuario = "primax",
                    IdTerminal = "9",
                    IdCompañia = "3"
                }
            };

            var mockTerminalCompañiaRepository = new Mock<IUsuariosTerminalCompañiaRepository>();
            mockTerminalCompañiaRepository.Setup(repo => repo.Get(It.IsAny<string>())).Returns(terminalesCompañia);
            return mockTerminalCompañiaRepository;
        }
        public static Mock<IUsuariosTerminalCompañiaRepository> GetUsuarioTerminalCompañiaDatosNulos()
        {
            List<TUUsuariosTerminalCompañia> terminalesCompañia = null;

            var mockTerminalCompañiaRepository = new Mock<IUsuariosTerminalCompañiaRepository>();
            mockTerminalCompañiaRepository.Setup(repo => repo.Get(It.IsAny<string>())).Returns(terminalesCompañia);
            return mockTerminalCompañiaRepository;
        }
    }
}
