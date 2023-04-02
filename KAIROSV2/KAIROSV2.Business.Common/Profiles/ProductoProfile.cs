using AutoMapper;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAIROSV2.Business.Common.Profiles
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<TProducto, ProductoTerminalDto>()
               .ForMember(
                   dest => dest.Asignado,
                   opt => opt.MapFrom(o => o.TProductosReceta.Any(r => r.TTerminalesProductosReceta.Count > 0)))
               .ForMember(
                    dest => dest.Icon,
                    opt => opt.MapFrom(o => o.IdClaseNavigation.Icono))
               .ForMember(
                    dest => dest.CodigoProducto,
                    opt => opt.MapFrom(o => o.IdProducto))
               .ForMember(
                dest => dest.Recetas,
                opt => opt.MapFrom(o => o.TProductosReceta));
        }
    }
}
