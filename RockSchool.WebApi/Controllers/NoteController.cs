using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Models;
using RockSchool.BL.Services.NoteService;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(NoteInfo addNoteDto)
        {
            var dt = addNoteDto.CompleteDate.Value.ToUniversalTime();

            await _noteService.AddNoteAsync(addNoteDto.BranchId, addNoteDto.Description, addNoteDto.CompleteDate.Value.ToUniversalTime());
            return Ok();
        }

        [HttpPut("{noteId}/edit")]
        public async Task<ActionResult> Update(Guid noteId, NoteInfo noteInfo)
        {
            var note = new Note
            {
                Status = (NoteStatus)noteInfo.Status,
                Description = noteInfo.Description,
                Comment = noteInfo.Comment,
                BranchId = noteInfo.BranchId,
                CompleteDate = noteInfo.CompleteDate?.ToUniversalTime(),
                ActualCompleteDate = noteInfo.ActualCompleteDate?.ToUniversalTime(),
                NoteId = noteId,
            };

            await _noteService.UpdateNoteAsync(note);

            return Ok();
        }

        [HttpPut("markComplete/{noteId}")]
        public async Task<ActionResult> MarkComplete(Guid noteId)
        {
            await _noteService.MarkComplete(noteId);
            return Ok();
        }
    }
}