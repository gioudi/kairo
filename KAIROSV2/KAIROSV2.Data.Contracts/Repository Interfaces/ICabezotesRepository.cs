using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ICabezotesRepository : IDataRepository<TCabezote>
    {
        IEnumerable<TCabezote> GetAll(params string[] includes);
        Task<TCabezote> Get(string placa);
        bool Exists(string placa);
    }
}
