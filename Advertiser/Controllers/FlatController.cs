using System.Collections.Generic;
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
        private readonly ServiceManager _serviceManager;
        public FlatController(ServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
        }
        [HttpGet]
        [Route("getAllFlats")]
        public async IAsyncEnumerable<FlatModel> GetFlats()
        {
            var flats = _serviceManager.Flats.GetAll();
            await foreach (var flat in flats)
            {
                yield return flat;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvert(IFormFile file, string price, string description)
        {
            return Ok();
        }
    }
}