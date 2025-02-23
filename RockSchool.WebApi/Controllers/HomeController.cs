using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.NoteService;

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

            var result = new 
            {
                Rooms = new[]
                {
                    new {roomName = "Зеленая", teacherName = "Сергей", studentName = "Акакий", status = "Занятие до 12:00"},
                    new {roomName = "Жёлтая", teacherName = "", studentName = "", status = "Свободно"},
                    new {roomName = "Вокальная", teacherName = "", studentName = "", status = "Свободно"},
                    new {roomName = "Гитарная", teacherName = "Михаил", studentName = "", status = "Репетиция до 14:00"},
                },
                Notes = notes,
            };

            return Ok(result);
        }
    }
}
