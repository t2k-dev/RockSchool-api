using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        [EnableCors("MyPolicy")]
        [HttpGet("getHomeScreenDetails")]
        public async Task<ActionResult> Get()
        {
            var result = new 
            {
                Rooms = new[]
                {
                    new {roomName = "Зеленая", teacherName = "Сергей", studentName = "Акакий", status = "Занятие до 12:00"},
                    new {roomName = "Жёлтая", teacherName = "", studentName = "", status = "Свободно"},
                    new {roomName = "Вокальная", teacherName = "", studentName = "", status = "Свободно"},
                    new {roomName = "Гитарная", teacherName = "Михаил", studentName = "", status = "Репетиция до 14:00"},
                },
                Notes = new[] 
                { 
                    new { description = "Пробный урок в 12:00", stasus = "Active"},
                    new { description = "Написать +77012031456 когда будут свободные окна у Аружан в 18.00", stasus = "Active"}
                },
            };

            return Ok(result);
        }
    }
}
