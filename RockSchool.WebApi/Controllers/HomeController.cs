using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Home;
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
    public class HomeController(
        INoteService noteService,
        IAttendanceService attendanceService,
        IBranchService branchService,
        IHomeService homeService
        ) : Controller
    {
        [EnableCors("MyPolicy")]
        [HttpGet("{branchId}")]
        public async Task<ActionResult> Get(int branchId)
        {
            var myResult = await homeService.GetByBranch(branchId);

            var result = new HomeScreenDetails
            {
                Branch = myResult.Branch,
                //Attendances = myResult.Attendances.ToInfos(),
                //Notes = notes.To,
            };

            return Ok(result);
        }
    }
}
