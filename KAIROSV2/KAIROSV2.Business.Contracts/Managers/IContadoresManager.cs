using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IContadoresManager
    {
        TContador ObtenerContador(string idContador );
        TContador ObtenerContador(string idContador, params string[] includes);
        Task<TContador> ObtenerContadorAsync(string idContador);
        IEnumerable<TContador> ObtenerContadores();
        IEnumerable<TContador> ObtenerContadores(params string[] includes);
        Task<IEnumerable<TContador>> ObtenerContadoresAsync();
        bool CrearContador(TContador Contador);
        Task<bool> CrearContadorAsync(TContador Contador);
        bool ActualizarContador(TContador Contador);
        Task<bool> ActualizarContadorAsync(TContador Contador);
        bool BorrarContador(string idContador );
        Task<bool> BorrarContadorAsync(string idContador);        
        IEnumerable<TProducto> ObtenerProductos();
        //IEnumerable<TContadoresTipo> ObtenerTiposContador();
        //IEnumerable<TContadoresEstados> ObtenerEstadosContador();


    }
}
