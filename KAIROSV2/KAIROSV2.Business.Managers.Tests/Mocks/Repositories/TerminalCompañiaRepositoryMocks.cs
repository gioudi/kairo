using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
    public class TerminalCompañiaRepositoryMocks
    {
        public static Mock<ITerminalCompañiaRepository> GetTerminalCompañia()
        {
            var terminal1 = new TTerminal()
            {
                IdTerminal = "1",
                Terminal = "Terminal 1"
            };

            var terminal2 = new TTerminal()
            {
                IdTerminal = "2",
                Terminal = "Terminal 2"
            };

            var terminal3 = new TTerminal()
            {
                IdTerminal = "3",
                Terminal = "Terminal 3"
            };

            var compañia1 = new TCompañia()
            {
                IdCompañia = "1",
                Nombre = "Compañia 1"
            };

            var compañia2 = new TCompañia()
            {
                IdCompañia = "2",
                Nombre = "Compañia 2"
            };

            var compañia3 = new TCompañia()
            {
                IdCompañia = "3",
                Nombre = "Compañia 3"
            };

            var terminalesCompañia = new List<TTerminalCompañia>()
            {
                new TTerminalCompañia()
                {
                    IdTerminal = "1",
                    IdCompañia = "1",
                    IdTerminalNavigation = terminal1,
                    IdCompañiaNavigation = compañia1
                },
                new TTerminalCompañia()
                {
                    IdTerminal = "1",
                    IdCompañia = "2",
                    IdTerminalNavigation = terminal1,
                    IdCompañiaNavigation = compañia2
                },
                 new TTerminalCompañia()
                {
                    IdTerminal = "2",
                    IdCompañia = "1",
                    IdTerminalNavigation = terminal2,
                    IdCompañiaNavigation = compañia1
                },
                 new TTerminalCompañia()
                {
                    IdTerminal = "2",
                    IdCompañia = "2",
                    IdTerminalNavigation = terminal2,
                    IdCompañiaNavigation = compañia2
                },
                 new TTerminalCompañia()
                {
                    IdTerminal = "2",
                    IdCompañia = "3",
                    IdTerminalNavigation = terminal2,
                    IdCompañiaNavigation = compañia3
                },
                 new TTerminalCompañia()
                {
                    IdTerminal = "3",
                    IdCompañia = "2",
                    IdTerminalNavigation = terminal3,
                    IdCompañiaNavigation = compañia2
                },
                 new TTerminalCompañia()
                {
                    IdTerminal = "3",
                    IdCompañia = "3",
                    IdTerminalNavigation = terminal3,
                    IdCompañiaNavigation = compañia3
                },
            };

            var mockTerminalCompañiaRepository = new Mock<ITerminalCompañiaRepository>();
            mockTerminalCompañiaRepository.Setup(repo => repo.Get(It.IsAny<string[]>())).Returns(terminalesCompañia);
            return mockTerminalCompañiaRepository;
        }

        public static Mock<ITerminalCompañiaRepository> GetTerminalCompañiaNulo()
        {

            List<TTerminalCompañia> terminalesCompañia = null;

            var mockTerminalCompañiaRepository = new Mock<ITerminalCompañiaRepository>();
            mockTerminalCompañiaRepository.Setup(repo => repo.Get(It.IsAny<string[]>())).Returns(terminalesCompañia);
            return mockTerminalCompañiaRepository;
        }
    }
}
