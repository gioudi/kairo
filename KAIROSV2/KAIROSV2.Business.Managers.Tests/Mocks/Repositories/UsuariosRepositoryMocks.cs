using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
    public class UsuariosRepositoryMocks
    {
        public static Mock<IUsuariosRepository> GetAddUpdateExists(bool exists)
        {
            var mockPermisosRepository = new Mock<IUsuariosRepository>();
            mockPermisosRepository.Setup(repo => repo.Exists(It.IsAny<string>())).Returns(exists);
            mockPermisosRepository.Setup(repo => repo.Add(It.IsAny<TUUsuario>()));
            mockPermisosRepository.Setup(repo => repo.Update(It.IsAny<TUUsuario>()));
            return mockPermisosRepository;
        }

    }
}
