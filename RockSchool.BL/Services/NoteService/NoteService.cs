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
                    CompleteDate = n.CompleteDate,
                    ActualCompleteDate = n.ActualCompleteDate,
                })
                .ToArray();

            return noteDtos;
        }

        public async Task<bool> AddNote(int branchId, string description, DateTime? completeDate)
        {
            var note = new NoteEntity
            {
                BranchId = branchId,
                Description = description,
                CompleteDate = completeDate,
                Status = 1,
            };

            return await _noteRepository.AddNote(note);
        }

        public async Task MarkComplete(Guid noteId)
        {
            var note = await _noteRepository.GetNote(noteId);
            note.Status = 2;
            note.ActualCompleteDate = DateTime.Now.ToUniversalTime();

            await _noteRepository.UpdateAsync(note);
        }
    }
}
