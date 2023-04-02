using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IDespachosComponentesRepository : IDataRepository<TDespachosComponente>
    {
        TDespachosComponente Obtener(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador);
        TDespachosComponente Obtener(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador, params string[] includes);
        Task<TDespachosComponente> ObtenerAsync(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador);
        IEnumerable<TDespachosComponente> ObtenerTodas();
        IEnumerable<TDespachosComponente> ObtenerTodas(params string[] includes);
        Task<IEnumerable<TDespachosComponente>> ObtenerTodasAsync(params string[] includes);
        Task<IEnumerable<TDespachosComponente>> ObtenerTodasAsync();        
        bool Existe(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador);
        Task<bool> ExisteAsync(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador);
        void Eliminar(string id);
        void Actualizar(TDespachosComponente Componente);
        Task ActualizarAsync(TDespachosComponente Componente, CancellationToken cancellationToken);

    }
}
