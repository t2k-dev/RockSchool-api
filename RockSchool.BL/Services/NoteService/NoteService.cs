using RockSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.NoteService
{
    public class NoteService : INoteService
    {
        private readonly NoteRepository _noteRepository;

        public NoteService(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<NoteDto[]?> GetNotesAsync(int branchId)
        {
            var notes = await _noteRepository.GetNotes(branchId);
            var noteDtos = notes.Select(n => new NoteDto
                {
                    NoteId = n.NoteId,
                    Description = n.Description,
                    Status = n.Status,
                    BranchId = n.BranchId,
                })
                .ToArray();

            return noteDtos;
        }
    }
}
