using AutoMapper;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Profiles
{
    public class ProveedorViewModelProfile : Profile
    {
        public ProveedorViewModelProfile()
        {
            CreateMap<GestionProveedorViewModel, TProveedor>()
                .ForMember(
                    dest => dest.TProveedoresPlanta,
                    opt => opt.MapFrom(o => o.ProveedoresPlanta))
                .ForMember(
                    dest => dest.TProveedoresProductos,
                    opt => opt.MapFrom(o => o.ProveedoresProductos))
                .ReverseMap();
        }
    }
}
