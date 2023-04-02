using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IAreasRepository : IDataRepository<TArea>
    {
        IEnumerable<TArea> ObtenerTodas(params string[] includes);
        IEnumerable<TArea> ObtenerTodas();
        TArea Obtener(string idArea, params string[] includes);
        bool Existe(string idArea);
        public TArea Obtener(string idArea);
        public Task<TArea> ObtenerAsync(string idArea);


    }
}
