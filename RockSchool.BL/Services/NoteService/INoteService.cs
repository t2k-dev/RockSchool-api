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
        Task<bool> AddNote(int branchId, string description);
    }
}
