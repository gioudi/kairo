using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Managers.Tests.Mocks
{
   public class DespachosRepositoryMocks
    {
        public static Mock<IDespachosRepository> GetExistAndRemove(bool exists)
        {
            var mockPermisosRepository = new Mock<IDespachosRepository>();
            mockPermisosRepository.Setup(repo => repo.Existe(It.IsAny<string>())).Returns(exists);
            mockPermisosRepository.Setup(repo => repo.Remove(It.IsAny<string>()));
            return mockPermisosRepository;
        }
    }
}
