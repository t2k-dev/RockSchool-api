using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.RoomService;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockSchool.WebApi.Helpers;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(
        IAttendanceService attendanceService,
        IRoomService roomService) : Controller
    {
        [HttpGet("{branchId}/busy-slots")]
        public async Task<ActionResult> GetBusySlots(int branchId)
        {
            var result = new List<RentableRoomDto>();

            var rentableRooms = await roomService.GetRentableRooms(branchId);
            foreach (var rentableRoom in rentableRooms)
            {
                var attendances = await attendanceService.GetByRoomIdAsync(rentableRoom.RoomId);
                attendances = attendances
                    .Where(a => a.Status != AttendanceStatus.CanceledByAdmin
                                && a.Status != AttendanceStatus.CanceledByStudent
                                && a.Status != AttendanceStatus.CanceledByTeacher
                                )
                    .ToArray();

                result.Add(
                    new RentableRoomDto
                    {
                        Id = rentableRoom.RoomId,
                        Name = rentableRoom.Name, 
                        Attendances = attendances.ToInfos()
                    });
            }

            return Ok(result);
        }
    }
}
