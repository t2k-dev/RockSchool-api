using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.BusySlotsService;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(IBusySlotsService busySlotsService) : Controller
    {
        [HttpGet("{branchId}/busy-slots")]
        public async Task<ActionResult> GetBusySlots(int branchId)
        {
            var busySlotsResult = await busySlotsService.GetBusySlotsByBranchAsync(branchId);
            
            // Map BL DTOs to WebApi DTOs
            var result = busySlotsResult.Select(slot => new RentableRoomDto
            {
                Id = slot.RoomId,
                Name = slot.RoomName,
                Attendances = slot.Attendances.ToInfos()
            }).ToArray();

            return Ok(result);
        }
    }
}
