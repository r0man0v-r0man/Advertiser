using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Models.FlatModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advertiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        private readonly ServiceManager serviceManager;
        public FlatController( ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet]
        [Route("allFlats")]
        public async IAsyncEnumerable<FlatModel> Get()
        {
            var flatsModels = serviceManager.Flats.GetAll();
            await foreach (var flatModel in flatsModels)
            {
                yield return flatModel;
            }
        }
    }
}