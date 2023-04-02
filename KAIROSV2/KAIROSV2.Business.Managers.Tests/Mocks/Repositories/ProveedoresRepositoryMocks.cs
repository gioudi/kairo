using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
   public class ProveedoresRepositoryMocks
    {
        public static Mock<IProveedoresRepository> GetExistAndRemove(bool exists)
        {
            var mockPermisosRepository = new Mock<IProveedoresRepository>();
            mockPermisosRepository.Setup(repo => repo.Exists(It.IsAny<string>())).Returns(exists);
            mockPermisosRepository.Setup(repo => repo.Remove(It.IsAny<string>()));
            return mockPermisosRepository;
        }
    }
}
