using AutoMapper;
using DataAccess.Entities;
using Domain.Models.FlatModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<Flat, FlatModel>().ReverseMap();
        }
    }
}
