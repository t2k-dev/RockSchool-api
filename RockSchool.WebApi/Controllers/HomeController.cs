using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Home;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(
        IHomeService homeService
        ) : Controller
    {
        [EnableCors("MyPolicy")]
        [HttpGet("{branchId}")]
        public async Task<ActionResult> Get(int branchId)
        {
            var myResult = await homeService.GetByBranchWithAttendees(branchId);

            var result = new HomeScreenDetails
            {
                Branch = myResult.Branch,
                Attendances = myResult.Attendances,
            };

            return Ok(result);
        }
    }
}
