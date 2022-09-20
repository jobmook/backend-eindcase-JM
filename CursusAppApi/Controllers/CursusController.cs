using CursusApp.Backend.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursusApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursusController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(CursusDto nieuweCursus)
        {
            if(nieuweCursus == null)
            {
                return BadRequest();
            } else
            {
                return Ok(nieuweCursus);
            }
        }
    }
}
