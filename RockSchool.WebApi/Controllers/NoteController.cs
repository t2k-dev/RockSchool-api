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
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<ActionResult> Add(AddNoteDto addNoteDto)
        {
            await _noteService.AddNoteAsync(addNoteDto.BranchId, addNoteDto.Description, addNoteDto.CompleteDate.Value.ToUniversalTime());
            return Ok();
        }

        [EnableCors("MyPolicy")]
        [HttpPut("markComplete/{noteId}")]
        public async Task<ActionResult> MarkComplete(Guid noteId)
        {
            await _noteService.MarkComplete(noteId);
            return Ok();
        }
    }
}
