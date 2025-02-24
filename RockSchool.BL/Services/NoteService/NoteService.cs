using RockSchool.Data.Repositories;
using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;

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

        public async Task<bool> AddNote(int branchId, string description)
        {
            var note = new NoteEntity
            {
                BranchId = branchId,
                Description = description,
                Status = 1,
            };

            return await _noteRepository.AddNote(note);
        }

        public async Task MarkComplete(Guid noteId)
        {
            var note = await _noteRepository.GetNote(noteId);
            note.Status = 2;
            await _noteRepository.UpdateAsync(note);
        }
    }
}
