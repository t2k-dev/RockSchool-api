using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        [HttpPost()]
        public async Task<ActionResult> Pay()
        {
            return Ok();
        }
    }
}
