using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok();
        }
    }
}