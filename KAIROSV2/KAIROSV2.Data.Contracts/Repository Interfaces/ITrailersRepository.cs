using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ITrailersRepository : IDataRepository<TTrailer>
    {
        IEnumerable<TTrailer> GetAll(params string[] includes);
        Task<TTrailer> Get(string placa);
        bool Exists(string placa);
    }
}
