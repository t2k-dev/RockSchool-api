using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.WebApi.Models.Subscriptions;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class RentalSubscriptionController(IRentalSubscriptionService rentalSubscriptionService) : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Add(AddRentalSubscriptionRequest request)
        {
            await rentalSubscriptionService.AddRentalSubscription(request.SubscriptionDetails, request.Schedules);

            return Ok();
        }
    }
}
