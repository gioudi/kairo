using AutoMapper;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Common.Profiles
{
    public class ProveedorPlantaProfile: Profile
    {
        public ProveedorPlantaProfile()
        {
            CreateMap<TProveedoresProducto, ProveedorProductoDTO>()
               .ForMember(
                   dest => dest.DescripcionTipo,
                   opt => opt.MapFrom(o => o.IdTipoProductoNavigation.Descripcion));

            CreateMap<ProveedorProductoDTO, TProveedoresProducto>();
        }
    }
}
