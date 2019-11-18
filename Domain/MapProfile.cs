using AutoMapper;
using DataAccess.Entities;
using Domain.Models.FlatModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Domain.Models.FileModels;

namespace Domain
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<FlatModel, Flat>()
                .ForMember(d => d.Image, s => s.MapFrom(src => src.File.LinkProps.Download))
                .ReverseMap();
            //CreateMap<Flat, FlatModel>()
            //    .ForMember(d => d.File.LinkProps.Download, s => s.MapFrom(src => src.Image))
            //    .ForMember(d => d.File.Name, s => s.MapFrom(src => Path.GetFileName(src.Image)))
            //    .ForMember(d => d.File.Uid, s => s.MapFrom(src => Path.GetFileNameWithoutExtension(src.Image)));
        }
    }
}
