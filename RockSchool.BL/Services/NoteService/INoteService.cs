using RockSchool.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Services.NoteService
{
    public interface INoteService
    {
        Task<NoteDto[]?> GetNotesAsync(int branchId);
        Task<bool> AddNoteAsync(int branchId, string description, DateTime? completeDate);
        Task MarkComplete(Guid noteId);
    }
}
