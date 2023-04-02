using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System.Linq;

namespace KAIROSV2.Business.Common.Profiles
{
    public class RecetasProfile : Profile
    {
        public RecetasProfile()
        {
            CreateMap<TProductosReceta, RecetaDTO>()
                .ForMember(
                    dest => dest.IdRecetaCurrent,
                    opt => opt.MapFrom(s => s.IdReceta))
                .ForMember(
                    dest => dest.Componentes,
                    opt => opt.MapFrom(c => c.TProductosRecetasComponentes));

            CreateMap<RecetaDTO, TProductosReceta>()
                .ForMember(
                    dest => dest.TProductosRecetasComponentes,
                    opt => opt.MapFrom(c => c.Componentes));

            CreateMap<TProductosRecetasComponente, RecetaComponenteDTO>()
                .ForMember(
                dest => dest.NombreProducto,
                opt => opt.MapFrom(c => c.IdComponenteNavigation.NombreCorto))
                .ForMember(
                dest => dest.Tipo,
                opt => opt.MapFrom(c => c.IdComponenteNavigation.IdTipoNavigation.Descripcion));

            CreateMap<RecetaComponenteDTO, TProductosRecetasComponente>();

            CreateMap<TProductosReceta, RecetaProductoTerminalDto>()
                .ForMember(
                    dest => dest.Asignada,
                    opt => opt.MapFrom(o => o.TTerminalesProductosReceta.Any()))
                .ForMember(
                    dest => dest.NombreReceta,
                    opt => opt.MapFrom(o => o.IdReceta))
                .ForMember(
                    dest => dest.Componentes,
                    opt => opt.MapFrom(c => c.TProductosRecetasComponentes))
                .ForMember(
                    dest => dest.Vigencias,
                    opt => opt.MapFrom(o => o.TTerminalesProductosReceta.Where(e => e.IdReceta == o.IdReceta)));

            CreateMap<TTerminalesProductosReceta, VigenciaRecetaDTO>()
               .ForMember(
                   dest => dest.FechaExpiracion,
                   opt => opt.MapFrom(o => o.FechaFin))
               .ForMember(
                   dest => dest.HoraInicio,
                   opt => opt.MapFrom(o => o.FechaInicio.TimeOfDay))
               .ForMember(
                   dest => dest.HoraExpiracion,
                   opt => opt.MapFrom<TimeSpan?>(o => o.FechaFin.HasValue ? (o.FechaFin.GetValueOrDefault().TimeOfDay) : null))
               .ForMember(
                   dest => dest.Activo,
                   opt => opt.MapFrom(o => (o.FechaInicio >= DateTime.Now && !o.FechaFin.HasValue) || (o.FechaInicio >= DateTime.Now && o.FechaFin.GetValueOrDefault() <= DateTime.Now)));

            CreateMap<VigenciaRecetaDTO, TTerminalesProductosReceta>()
                .ForMember(
                    dest => dest.FechaInicio,
                    opt => opt.MapFrom(o => o.FechaInicio.GetValueOrDefault().Date + o.HoraInicio.GetValueOrDefault()))
                .ForMember(
                    dest => dest.FechaFin,
                    opt => opt.MapFrom<DateTime?>(o => o.FechaExpiracion.HasValue ? (o.FechaExpiracion.GetValueOrDefault().Date + o.HoraExpiracion.GetValueOrDefault()) : null))
                .ForMember(
                    dest => dest.UltimaEdicion,
                    opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(
                    dest => dest.EditadoPor,
                    opt => opt.MapFrom(o => "Admin"));
        }
    }
}
