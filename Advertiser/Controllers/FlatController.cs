using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Models.FileModels;
using Domain.Models.FlatModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advertiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        private readonly ServiceManager _serviceManager;
        public FlatController(ServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
        }
        [HttpGet("getAllFlats")]
        public async IAsyncEnumerable<FlatModel> GetFlats()
        {
            var flats = _serviceManager.Flats.GetAll();
            await foreach (var flat in flats)
            {
                yield return flat;
            }
        }

        [HttpPost("createFlatAdvert")]
        public async Task<IActionResult> CreateAdvert(FlatModel flatModel)
        {
            await _serviceManager.Flats.AddAsync(flatModel).ConfigureAwait(false);
            return Ok();
        }
    }
}