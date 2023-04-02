using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers
{
    public class ConductoresManager : ManagerBase, IConductoresManager
    {
        private readonly IConductoresRepository _ConductoresRepository;

        public ConductoresManager(IConductoresRepository ConductoresRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ConductoresRepository = ConductoresRepository;
        }

        public async Task<TConductor> ObtenerConductorAsync(int Cedula)
        {
            return await _ConductoresRepository.Get(Cedula);
        }

        public TConductor ObtenerConductor(int Cedula)
        {
            return _ConductoresRepository.ObtenerConductor(Cedula);
        }
        public IEnumerable<TConductor> ObtenerConductores()
        {
            return _ConductoresRepository.GetAll();
        }

        public bool CrearConductor(TConductor Conductores)
        {
            if (_ConductoresRepository.Exists(Conductores.Cedula))
                return false;
            else
            {
                _ConductoresRepository.Add(Conductores);
                LogInformacion(LogAcciones.Insertar, "Suministro y logística", "Conductores", "Conductores", "T_Conductores", "Conductor " + Conductores?.Cedula + " creado");
            }

            return true;
        }

        public bool ActualizarConductor(TConductor Conductores)
        {
            if (!_ConductoresRepository.Exists(Conductores.Cedula))
                return false;
            else
            {
                var conductor = _ConductoresRepository.ObtenerConductor(Conductores.Cedula);
                conductor.Nombre = Conductores.Nombre;
                _ConductoresRepository.Update(conductor);
                LogInformacion(LogAcciones.Actualizar, "Suministro y logística", "Conductores", "Conductores", "T_Conductores", "Conductor " + Conductores?.Cedula + " actualizado");
            }

            return true;
        }

        public async Task<bool> BorrarConductor(TConductor Conductores)
        {
            if (!_ConductoresRepository.Exists(Conductores.Cedula))
                return false;
            else
            {
                _ConductoresRepository.Remove(Conductores);
                LogInformacion(LogAcciones.Eliminar, "Suministro y logística", "Conductores", "Conductores", "T_Conductores", "Conductor " + Conductores.Cedula + " eliminado");
            }

            return true;
        }
    }

}
