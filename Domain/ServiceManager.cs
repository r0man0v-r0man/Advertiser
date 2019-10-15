using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ServiceManager
    {
        private readonly IFlatService flatService;
        public ServiceManager(IFlatService flatService)
        {
            this.flatService = flatService;
        }
        public IFlatService Flats { get => flatService; }

    }
}
