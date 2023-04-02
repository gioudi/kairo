using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IConductoresManager
    {
        Task<TConductor> ObtenerConductorAsync(int Cedula);
        IEnumerable<TConductor> ObtenerConductores();
        bool CrearConductor(TConductor Conductores);
        bool ActualizarConductor(TConductor Conductores);
        Task<bool> BorrarConductor(TConductor Conductores);
        TConductor ObtenerConductor(int Cedula);
    }
}
