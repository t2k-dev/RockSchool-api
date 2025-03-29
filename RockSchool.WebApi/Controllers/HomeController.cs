using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.NoteService;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly INoteService _noteService;

        public HomeController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [EnableCors("MyPolicy")]
        [HttpGet("getHomeScreenDetails/{branchId}")]
        public async Task<ActionResult> Get(int branchId)
        {
            var notes = await _noteService.GetNotesAsync(branchId);

            var result = new HomeScreenDetails
            {
                Attendances = new[]
                {
                    new AttendanceInfo
                    {
                        AttendanceId = Guid.NewGuid(),
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
                        DisciplineId = 5,
                        Status = 1,
                        RoomId = 1,
                        Student = new
                        {
                            FirstName = "Алексей",
                            LastName = "Кутузов",
                        },
                        Teacher = new
                        {
                            FirstName = "Варвара",
                        },
                    },
                    new AttendanceInfo
                    {
                        AttendanceId = Guid.NewGuid(),
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0),
                        DisciplineId = 1,
                        Status = 1,
                        RoomId = 3,
                        Student = new
                        {
                            FirstName = "Назар",
                            LastName = "Рахимжанов",
                        },
                        Teacher = new
                        {
                            FirstName = "Оспан",
                        },
                    },
                },
                Notes = notes,
            };


            return Ok(result);
        }
    }
}
