using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.AttendanceService;
using System.Threading.Tasks;
using RockSchool.Data.Enums;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(
        IAttendanceService attendanceService) : Controller
    {
        [HttpGet("{branchId}/busy-slots")]
        public async Task<ActionResult> GetBusySlots(int branchId)
        {
            var attendances = await attendanceService.GetByBranchIdAsync(branchId);
            attendances = attendances
                .Where(a => a.Status != AttendanceStatus.CanceledByAdmin 
                            && a.Status != AttendanceStatus.CanceledByStudent
                            && a.Status != AttendanceStatus.CanceledByTeacher)
                .ToArray();

            return Ok(new { attendances = attendances });
        }
    }
}
