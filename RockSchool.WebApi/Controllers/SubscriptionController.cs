using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<ActionResult> Add(SubscriptionDto subscriptionDtoDto)
        {
            // TODO: Implement
            // TODO: Subscription - add trial method
            return Ok("Test");
        }

    }
}
