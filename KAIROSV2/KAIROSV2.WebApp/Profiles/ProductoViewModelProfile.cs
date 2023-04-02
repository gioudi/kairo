using AutoMapper;
using KAIROSV2.Business.Entities;
using KAIROSV2.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Profiles
{
    public class ProductoViewModelProfile : Profile
    {
        public ProductoViewModelProfile()
        {
            CreateMap<GestionProductosViewModel, TProducto>()
                .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(o => (o.Estado ? "1" : "0")))
                .ForMember(
                    dest => dest.TProductosReceta,
                    opt => opt.MapFrom(o => o.Recetas))
                .ForMember(
                    dest => dest.UltimaEdicion,
                    opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(
                    dest => dest.EditadoPor,
                    opt => opt.MapFrom(o => "Admin"));

            CreateMap<TProducto, GestionProductosViewModel>()
                 .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(o => (o.Estado.ToLower() == "1" ? true : false)))
                .ForMember(
                    dest => dest.Recetas,
                    opt => opt.MapFrom(o => o.TProductosReceta))
                .ForMember(
                dest => dest.Icono,
                opt => opt.MapFrom(o => o.IdClaseNavigation.Icono));
        }
    }
}
