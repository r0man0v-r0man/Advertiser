using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ServiceManager
    {
        private readonly IFlatService _flatService;
        private readonly IFileService _fileService;
        public ServiceManager(IFlatService flatService, IFileService fileService)
        {
            _flatService = flatService;
            _fileService = fileService;
        }
        public IFlatService Flats { get => _flatService; }
        public IFileService Files { get => _fileService; }
    }
}
