using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IConductoresRepository : IDataRepository<TConductor>
    {
        IEnumerable<TConductor> GetAll(params string[] includes);
       
        IEnumerable<TConductor> GetAll();

        Task<TConductor> Get(int Cedula, params string[] includes);

        Task<TConductor> Get(int Cedula);
        
        bool Exists(int Cedula);
        TConductor ObtenerConductor(int Cedula);
    }
}
