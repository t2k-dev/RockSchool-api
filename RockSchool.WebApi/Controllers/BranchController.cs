using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Bands;
using RockSchool.BL.Services.BusySlotsService;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(IBusySlotsService busySlotsService, IBandService bandService) : Controller
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


        [HttpGet("{branchId}/active-bands")]
        public async Task<ActionResult> GetActiveBandsByBranchId(int branchId)
        {
            var bands = await bandService.GetActiveByBranchIdAsync(branchId);
            var result = bands.ToInfos();
            return Ok(result);
        }
    }
}
