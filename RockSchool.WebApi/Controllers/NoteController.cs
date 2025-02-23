using Microsoft.AspNetCore.Mvc;

namespace RockSchool.WebApi.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
