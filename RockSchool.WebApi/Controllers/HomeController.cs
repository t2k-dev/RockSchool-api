using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.BranchService;
using RockSchool.BL.Services.NoteService;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly INoteService _noteService;
        private readonly IAttendanceService _attendanceService;
        private readonly IBranchService _branchService;

        public HomeController(INoteService noteService, IAttendanceService attendanceService, IBranchService branchService)
        {
            _noteService = noteService;
            _attendanceService = attendanceService;
            _branchService = branchService;
        }

        [EnableCors("MyPolicy")]
        [HttpGet("getHomeScreenDetails/{branchId}")]
        public async Task<ActionResult> Get(int branchId)
        {
            var branch = await _branchService.GetBranchByIdAsync(branchId);

            var notes = await _noteService.GetNotesAsync(branchId);

            var allAttendances = await _attendanceService.GetByBranchIdAsync(branchId);

            var attendanceInfos = allAttendances.Where(a => a.GroupId == null).ToParentAttendanceInfos();
            var groupAttendanceInfos = AttendanceBuilder.BuildGroupAttendanceInfos(allAttendances.Where(a => a.GroupId != null));
            attendanceInfos.AddRange(groupAttendanceInfos);

            var result = new HomeScreenDetails
            {
                Branch = branch,
                Attendances = attendanceInfos.ToArray(),
                Notes = notes,
            };

            return Ok(result);
        }
    }
}
