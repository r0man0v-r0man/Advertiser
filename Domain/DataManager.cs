using DataAccess.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DataManager
    {
        public DataManager(IRepository<Flat> flatRepository)
        {
            this.Flats = flatRepository;
        }
        public IRepository<Flat> Flats { get; }
    }
}
